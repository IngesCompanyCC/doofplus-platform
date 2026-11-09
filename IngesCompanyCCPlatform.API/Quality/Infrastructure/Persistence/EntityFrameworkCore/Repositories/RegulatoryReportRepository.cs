using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the regulatory report repository of the Quality &amp; Compliance bounded context.
/// </summary>
/// <param name="context">The <see cref="AppDbContext" /> to use.</param>
public class RegulatoryReportRepository(AppDbContext context)
    : BaseRepository<RegulatoryReport>(context), IRegulatoryReportRepository
{
    // <inheritdoc />
    public async Task<IReadOnlyCollection<RegulatoryReport>> FindByOrganizationIdAsync(OrganizationId organizationId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<RegulatoryReport>()
            .AsNoTracking()
            .Where(report => report.OrganizationId == organizationId)
            .OrderByDescending(report => report.GeneratedAt)
            .ToListAsync(cancellationToken);
    }

    // <inheritdoc />
    public new async Task<RegulatoryReport?> FindByIdAsNoTrackingAsync(int id,
        CancellationToken cancellationToken = default)
    {
        var reportId = new RegulatoryReportId(id);
        return await Context.Set<RegulatoryReport>()
            .AsNoTracking()
            .FirstOrDefaultAsync(report => report.Id == reportId, cancellationToken);
    }
}
