using System.ComponentModel.DataAnnotations;
using System.Text.Json;

using FluentValidation;

using MapsterMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using StixVuln.Api.DTO.Vulnerability;
using StixVuln.Core;
using StixVuln.Core.Vulnerability;
using StixVuln.Core.Vulnerability.Interfaces;

namespace StixVuln.Api.Controllers;

[ApiController]
[Route("api/vulnerabilities")]
[Authorize]
public class VulnerabilitiesController : ControllerBase
{
    private readonly IVulnerabiltiesRepository _vulnerabiltiesRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateVulnerabilityDTO> _createVulnerabilityValidator;
    private readonly IValidator<UpdateVulnerabilityDTO> _updateVulnerabilityValidator;

    public VulnerabilitiesController(
        IVulnerabiltiesRepository vulnerabiltiesRepository,
        IMapper mapper,
        IValidator<CreateVulnerabilityDTO> createVulnerabilityValidator,
        IValidator<UpdateVulnerabilityDTO> updateVulnerabilityValidator)
    {
        _vulnerabiltiesRepository = vulnerabiltiesRepository;
        _mapper = mapper;
        _createVulnerabilityValidator = createVulnerabilityValidator;
        _updateVulnerabilityValidator = updateVulnerabilityValidator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        string? name,
        string? search,
        string? orderBy,
        [Range(1, 100)] int pageSize = 10,
        [Range(1, 100)] int pageNumber = 1)
    {
        var (vulnerabilities, paginationMetaData) =
            await _vulnerabiltiesRepository.ListAsync(
                name,
                search,
                orderBy,
                pageSize,
                pageNumber);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

        return Ok(_mapper.Map<List<VulnerabilityResponseDTO>>(vulnerabilities));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var vulnerability = await _vulnerabiltiesRepository.GetByIdAsync(id);

        if (vulnerability == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<VulnerabilityResponseDTO>(vulnerability));
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        CreateVulnerabilityDTO request)
    {
        var validationResult = _createVulnerabilityValidator.Validate(request);

        if (!validationResult.IsValid) {
            return BadRequest(validationResult.Errors);
        }

        var userIdentity = User.Claims.FirstOrDefault(c => c.Type == "identity")?.Value;

        var newVulnerability = new Vulnerability(
            request.Name,
            request.Description,
            userIdentity
            );

        newVulnerability.SetExternalReferences(
            _mapper.Map<List<ExternalReference>>(
                request.ExternalReferences ?? new()));

        return Ok(_mapper.Map<VulnerabilityResponseDTO>(
            await _vulnerabiltiesRepository.AddAsync(newVulnerability)));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(
        string id,
        [FromBody] UpdateVulnerabilityDTO request)
    {
        var validationResult = _updateVulnerabilityValidator.Validate(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var vulnerabilityToUpdate =
            await _vulnerabiltiesRepository.GetByIdAsync(id);

        if (vulnerabilityToUpdate == null)
        {
            return NotFound();
        }

        vulnerabilityToUpdate.UpdateNameAndDescription(
            request.Name,
            request.Description);
        vulnerabilityToUpdate.SetExternalReferences(
            _mapper.Map<List<ExternalReference>>(request.ExternalReferences ?? new()));
        vulnerabilityToUpdate.Modified = DateTime.UtcNow;

        await _vulnerabiltiesRepository.UpdateAsync(vulnerabilityToUpdate);

        return Ok();
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var vulnerabilityToDelete =
            await _vulnerabiltiesRepository.GetByIdAsync(id);

        if (vulnerabilityToDelete == null)
        {
            return NotFound();
        }

        await _vulnerabiltiesRepository.DeleteAsync(vulnerabilityToDelete);

        return Ok();
    }
}