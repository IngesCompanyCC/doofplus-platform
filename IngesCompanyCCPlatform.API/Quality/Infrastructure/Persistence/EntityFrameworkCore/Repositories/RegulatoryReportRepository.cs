using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Quality.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="RegulatoryReport" />.
/// </summary>
public class RegulatoryReportRepository(AppDbContext context) : BaseRepository<RegulatoryReport>(context), IRegulatoryReportRepository
{
    /// <inheritdoc />
    public new async Task<RegulatoryReport?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var regulatoryReportId = new RegulatoryReportId(id);
        return await Context.Set<RegulatoryReport>().FirstOrDefaultAsync(regulatoryReport => regulatoryReport.Id == regulatoryReportId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<RegulatoryReport?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var regulatoryReportId = new RegulatoryReportId(id);
        return await Context.Set<RegulatoryReport>()
            .AsNoTracking()
            .FirstOrDefaultAsync(regulatoryReport => regulatoryReport.Id == regulatoryReportId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<RegulatoryReport>().AnyAsync(regulatoryReport => regulatoryReport.Code == code, cancellationToken);
    }
}
