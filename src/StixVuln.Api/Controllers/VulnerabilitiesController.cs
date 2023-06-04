using MapsterMapper;

using Microsoft.AspNetCore.Mvc;

using StixVuln.Api.DTO;
using StixVuln.Core;
using StixVuln.Core.Vulnerability;
using StixVuln.Core.Vulnerability.Interfaces;
using StixVuln.Infrastructure.Data;

namespace StixVuln.Api.Controllers;

[ApiController]
[Route("api/vulnerabilities")]
public class VulnerabilitiesController : ControllerBase
{
    private readonly IVulnerabiltiesRepository _vulnerabiltiesRepository;
    private readonly IMapper _mapper;

    public VulnerabilitiesController(
        IVulnerabiltiesRepository vulnerabiltiesRepository, 
        IMapper mapper)
    {
        _vulnerabiltiesRepository = vulnerabiltiesRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _vulnerabiltiesRepository.ListAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await _vulnerabiltiesRepository.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] CreateVulnerabilityDTO request)
    {
        var newVulnerability = new Vulnerability(
            request.Name,
            request.Description,
            null
            );

        newVulnerability.SetExternalReferences(
            _mapper.Map<List<ExternalReference>>(
                request.ExternalReferences));

        return Ok(await _vulnerabiltiesRepository.AddAsync(
            newVulnerability));
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(
        string id, 
        [FromBody] UpdateVulnerabilityDTO request)
    {
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
            _mapper.Map<List<ExternalReference>>(request.ExternalReferences));
        vulnerabilityToUpdate.Modified = DateTime.UtcNow;

        await _vulnerabiltiesRepository.UpdateAsync(vulnerabilityToUpdate);

        return Ok();
    }

    [HttpDelete]
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