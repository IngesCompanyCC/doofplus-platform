using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;

/// <summary>
///     Repository of the telemetry readings.
/// </summary>
public interface IReadingRepository : IBaseRepository<Reading>
{
    /// <summary>
    ///     Gets the readings of a sensor, the newest first.
    /// </summary>
    Task<IEnumerable<Reading>> FindAllBySensorCodeAsNoTrackingAsync(SensorCode sensorCode, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the readings recorded during a production batch, the newest first.
    /// </summary>
    Task<IEnumerable<Reading>> FindAllByBatchCodeAsNoTrackingAsync(string batchCode, CancellationToken cancellationToken = default);
}
