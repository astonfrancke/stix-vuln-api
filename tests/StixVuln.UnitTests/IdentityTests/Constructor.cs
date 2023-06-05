using StixVuln.Core.Identity;

using Xunit;

namespace StixVuln.UnitTests.IdentityTests;
public class Constructor
{
    private readonly string identityName = "Identity1";
    private readonly string identityDesc = "This is a new identity";
    private readonly string identityContactInfo = "12345678910";
    private readonly string identityClass = "12345678910";
    private readonly List<string> identityRoles = new() { "Org", "Admin" };
    private readonly List<string> identitySectors = new() { "Finance", "Health" };

    [Fact]
    public void ShouldCreateNewIdentity_GivenCorrectParameters()
    {
        var identityCreation =
            () => new Identity(
                identityName,
                identityDesc,
                identityContactInfo,
                identityClass,
                identityRoles,
                identitySectors);
        
        Assert.True(true);
    }

    [Fact]
    public void ShouldThrowArgumentNullException_GivenIdentityEmptyName()
    {
        var identityCreation =
            () => new Identity(
                "",
                identityDesc,
                identityContactInfo,
                identityClass,
                identityRoles,
                identitySectors);
        Assert.Throws<ArgumentNullException>(identityCreation);
    }
}