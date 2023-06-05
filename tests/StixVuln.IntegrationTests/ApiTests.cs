using System.Text;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc.Testing;

using StixVuln.Api.DTO.Authentication;
using StixVuln.Api.DTO.Vulnerability;
using StixVuln.Api.Extensions;

namespace StixVuln.IntegrationTests;
public class ApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly string _authUrl = "/api/auth/login";

    public ApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private async Task<string?> GetUserToken(HttpClient client)
    {
        var body = new LoginRequestDTO(
            "user1",
            "password1");

        var serializedBody = JsonSerializer.Serialize(body);
        var content = new StringContent(serializedBody, encoding: Encoding.UTF8, "application/json");
        var response = await client.PostAsync(_authUrl, content);

        var responseDeserialized = JsonSerializer.Deserialize<LoginResponseDTO>(await response.Content.ReadAsStringAsync());
        return responseDeserialized?.token;
    }

    private async Task<string?> GetAdminToken(HttpClient client)
    {
        var body = new LoginRequestDTO(
            "admin1",
            "password1");

        var serializedBody = JsonSerializer.Serialize(body);
        var content = new StringContent(serializedBody, encoding: Encoding.UTF8, "application/json");
        var response = await client.PostAsync(_authUrl, content);

        var responseDeserialized = JsonSerializer.Deserialize<LoginResponseDTO>(await response.Content.ReadAsStringAsync());
        return responseDeserialized?.token;
    }

    [Theory]
    [InlineData("/api/vulnerabilities", 3)]
    [InlineData("/api/vulnerabilities?pageSize=1&orderBy=name", 1)]
    [InlineData("/api/vulnerabilities?name=CVE-2016-1234&orderBy=name", 1)]
    [InlineData("/api/vulnerabilities?search=Crit&orderBy=name", 1)]
    public async Task GetVulnerabilities(string url, int totalCount)
    {
        // Arrange
        var client = _factory.CreateClient();
        var token = await GetUserToken(client);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        var vulnerabilities = JsonSerializer.Deserialize<List<VulnerabilityResponseDTO>>(await response.Content.ReadAsStringAsync(), options: new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy()
        });
        Assert.Equal(totalCount, vulnerabilities.Count());
    }

    [Theory]
    [InlineData("/api/vulnerabilities", "TestVulnName", "TestVulnDescription", "TestExRefSourceName", "TestExRefDescription")]
    public async Task AddVulnerability(string url, string vulnName, string vulnDescription, string vulnExRefSourceName, string vulnExRefDescription)
    {
        // Arrange
        var client = _factory.CreateClient();
        var token = await GetUserToken(client);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var body = new CreateVulnerabilityDTO(
            vulnName,
            vulnDescription,
            new List<ExternalReferenceRequestDTO> { new ExternalReferenceRequestDTO(vulnExRefSourceName, vulnExRefDescription, null, null, null) });

        var serializedBody = JsonSerializer.Serialize(body, options: new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy()
        });
        var content = new StringContent(serializedBody, encoding: Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync(url, content);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        var vulnerability = JsonSerializer.Deserialize<VulnerabilityResponseDTO>(await response.Content.ReadAsStringAsync(), options: new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy()
        });

        Assert.NotNull(vulnerability);
        Assert.Equal(vulnName, vulnerability.Name);
        Assert.Equal(vulnDescription, vulnerability.Description);
        Assert.Equal(vulnExRefSourceName, vulnerability.ExternalReferences[0].SourceName);
    }

    [Theory]
    [InlineData("/api/vulnerabilities", "TestUpdateVulnDescription")]
    public async Task UpdateVulnerability(string url, string vulnUpdateDesc)
    {
        // Arrange
        var client = _factory.CreateClient();
        var token = await GetUserToken(client);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");


        var allVulnerabilitiesResponse = await client.GetAsync(url);

        allVulnerabilitiesResponse.EnsureSuccessStatusCode(); // Status Code 200-299
        var vulnerabilities = JsonSerializer.Deserialize<List<VulnerabilityResponseDTO>>(await allVulnerabilitiesResponse.Content.ReadAsStringAsync(), options: new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy()
        });
        
        Assert.NotNull(vulnerabilities);
        var vulnToUpdate = vulnerabilities.FirstOrDefault();
        Assert.NotNull(vulnToUpdate);

        // Act
        var body = new UpdateVulnerabilityDTO(
            vulnToUpdate.Name,
            vulnUpdateDesc,
            new List<ExternalReferenceRequestDTO> { new ExternalReferenceRequestDTO(vulnToUpdate.ExternalReferences[0]?.SourceName, vulnToUpdate.ExternalReferences[0]?.Description, null, null, null) });

        var serializedBody = JsonSerializer.Serialize(body, options: new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy()
        });
        var content = new StringContent(serializedBody, encoding: Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"{url}/{vulnToUpdate.Id}", content);

        // Assert
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task DeleteVulnerability()
    {
        var url = "/api/vulnerabilities";

        // Arrange
        var client = _factory.CreateClient();
        var token = await GetUserToken(client);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var allVulnerabilitiesResponse = await client.GetAsync(url);

        allVulnerabilitiesResponse.EnsureSuccessStatusCode(); // Status Code 200-299
        var vulnerabilities = JsonSerializer.Deserialize<List<VulnerabilityResponseDTO>>(await allVulnerabilitiesResponse.Content.ReadAsStringAsync(), options: new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy()
        });

        Assert.NotNull(vulnerabilities);
        var vulnToDelete = vulnerabilities.FirstOrDefault();
        Assert.NotNull(vulnToDelete);

        var response = await client.DeleteAsync($"{url}/{vulnToDelete.Id}");

        Assert.False(response.IsSuccessStatusCode);

        client.DefaultRequestHeaders.Remove("Authorization");
        token = await GetAdminToken(client);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        response = await client.DeleteAsync($"{url}/{vulnToDelete.Id}");

        Assert.True(response.IsSuccessStatusCode);
    }
}
