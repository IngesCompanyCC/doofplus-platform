using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Monitoring.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="Alert" />.
/// </summary>
public class AlertRepository(AppDbContext context) : BaseRepository<Alert>(context), IAlertRepository
{
    /// <inheritdoc />
    public new async Task<Alert?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var alertId = new AlertId(id);
        return await Context.Set<Alert>().FirstOrDefaultAsync(alert => alert.Id == alertId, cancellationToken);
    }

    /// <inheritdoc />
    public new async Task<Alert?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var alertId = new AlertId(id);
        return await Context.Set<Alert>()
            .AsNoTracking()
            .FirstOrDefaultAsync(alert => alert.Id == alertId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByCodeAsync(AlertCode code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>().AnyAsync(alert => alert.Code == code, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Alert?> FindByCodeAsNoTrackingAsync(AlertCode code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>()
            .AsNoTracking()
            .FirstOrDefaultAsync(alert => alert.Code == code, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Alert>> FindAllBySensorCodeAsNoTrackingAsync(string sensorCode, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>()
            .AsNoTracking()
            .Where(alert => alert.SensorCode == sensorCode)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Alert>> FindAllByBatchCodeAsNoTrackingAsync(string batchCode, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>()
            .AsNoTracking()
            .Where(alert => alert.BatchCode == batchCode)
            .ToListAsync(cancellationToken);
    }
}
