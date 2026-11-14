using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;

/// <summary>
///     Repository of the alerts.
/// </summary>
public interface IAlertRepository : IBaseRepository<Alert>
{
    /// <summary>
    ///     Checks whether an alert already has the code.
    /// </summary>
    Task<bool> ExistsByCodeAsync(AlertCode code, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds an alert by its code, without tracking it.
    /// </summary>
    Task<Alert?> FindByCodeAsNoTrackingAsync(AlertCode code, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the alerts raised by a sensor.
    /// </summary>
    Task<IEnumerable<Alert>> FindAllBySensorCodeAsNoTrackingAsync(string sensorCode, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the alerts raised during a production batch.
    /// </summary>
    Task<IEnumerable<Alert>> FindAllByBatchCodeAsNoTrackingAsync(string batchCode, CancellationToken cancellationToken = default);
}
