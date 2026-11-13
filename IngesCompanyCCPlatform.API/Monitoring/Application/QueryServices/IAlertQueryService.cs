using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.QueryServices;

/// <summary>
///     Query service of the alerts.
/// </summary>
public interface IAlertQueryService
{
    /// <summary>
    ///     Gets all the alerts.
    /// </summary>
    Task<IEnumerable<Alert>> Handle(GetAllAlertsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets the alerts of a sensor.
    /// </summary>
    Task<IEnumerable<Alert>> Handle(GetAllAlertsBySensorCodeQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets the alerts of a production batch.
    /// </summary>
    Task<IEnumerable<Alert>> Handle(GetAllAlertsByBatchCodeQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets an alert by its id, or null.
    /// </summary>
    Task<Alert?> Handle(GetAlertByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets an alert by its code, or null.
    /// </summary>
    Task<Alert?> Handle(GetAlertByCodeQuery query, CancellationToken cancellationToken);
}
