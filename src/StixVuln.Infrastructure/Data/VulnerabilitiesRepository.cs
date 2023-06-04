using Microsoft.EntityFrameworkCore;

using StixVuln.Core.Vulnerability;
using StixVuln.Core.Vulnerability.Interfaces;

namespace StixVuln.Infrastructure.Data;
public class VulnerabilitiesRepository : IVulnerabiltiesRepository
{
    private readonly AppDbContext _appDbContext;

    public VulnerabilitiesRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Vulnerability> AddAsync(Vulnerability vulnerability, CancellationToken cancellationToken = default)
    {
        _appDbContext.Vulnerabilities.Add(vulnerability);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return vulnerability;
    }

    public async Task DeleteAsync(Vulnerability vulnerability, CancellationToken cancellationToken = default)
    {
        _appDbContext.Vulnerabilities.Remove(vulnerability);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Vulnerability?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Vulnerabilities.FirstOrDefaultAsync(v => v.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Vulnerability>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Vulnerabilities.ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Vulnerability vulnerability, CancellationToken cancellationToken = default)
    {
        _appDbContext.Vulnerabilities.Update(vulnerability);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}
