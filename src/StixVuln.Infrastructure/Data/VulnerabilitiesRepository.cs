using Microsoft.EntityFrameworkCore;

using StixVuln.Core.Common;
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
    public async Task<(IEnumerable<Vulnerability>, PaginationMetaData)> ListAsync(
        string? name,
        string? search,
        string? orderBy,
        int pageSize,
        int pageNumber,
        CancellationToken cancellationToken = default)
    {
        var vulnCollection = _appDbContext.Vulnerabilities as IQueryable<Vulnerability>;

        if (!string.IsNullOrWhiteSpace(name))
        {
            name = name.Trim();
            vulnCollection = vulnCollection.Where(v => v.Name == name);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            vulnCollection = vulnCollection.Where(
                v => v.Name.Contains(search) ||
                v.Description != null && v.Description.Contains(search));
        }

        var totalVulnCount = await vulnCollection.CountAsync();

        var paginationMetaData = new PaginationMetaData(
            totalVulnCount,
            pageSize,
            pageNumber);

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            orderBy = orderBy.Trim().ToLower();
            switch (orderBy)
            {
                case "name":
                    vulnCollection = vulnCollection.OrderBy(v => v.Name);
                    break;
                case "description":
                    vulnCollection = vulnCollection.OrderBy(v => v.Description);
                    break;
                case "modified":
                    vulnCollection = vulnCollection.OrderBy(v => v.Modified);
                    break;
                case "created_by":
                    vulnCollection = vulnCollection.OrderBy(v => v.CreatedByRef);
                    break;
                default:
                    vulnCollection = vulnCollection.OrderBy(v => v.Created);
                    break;
            }
        }

        return (await vulnCollection
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken), paginationMetaData);
    }

    public async Task UpdateAsync(Vulnerability vulnerability, CancellationToken cancellationToken = default)
    {
        _appDbContext.Vulnerabilities.Update(vulnerability);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}
