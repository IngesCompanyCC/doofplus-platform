using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.QueryServices;

/// <summary>
///     Query service of the telemetry readings.
/// </summary>
public interface IReadingQueryService
{
    /// <summary>
    ///     Gets all the readings.
    /// </summary>
    Task<IEnumerable<Reading>> Handle(GetAllReadingsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets the readings of a sensor.
    /// </summary>
    Task<IEnumerable<Reading>> Handle(GetAllReadingsBySensorCodeQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets the readings of a production batch.
    /// </summary>
    Task<IEnumerable<Reading>> Handle(GetAllReadingsByBatchCodeQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a reading by its id, or null.
    /// </summary>
    Task<Reading?> Handle(GetReadingByIdQuery query, CancellationToken cancellationToken);
}
