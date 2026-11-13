using IngesCompanyCCPlatform.API.Monitoring.Application.QueryServices;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="Alert" />.
/// </summary>
public class AlertQueryService(IAlertRepository alertRepository) : IAlertQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Alert>> Handle(GetAllAlertsQuery query, CancellationToken cancellationToken)
    {
        return await alertRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Alert>> Handle(GetAllAlertsBySensorCodeQuery query, CancellationToken cancellationToken)
    {
        return await alertRepository.FindAllBySensorCodeAsNoTrackingAsync(query.SensorCode.Value, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Alert>> Handle(GetAllAlertsByBatchCodeQuery query, CancellationToken cancellationToken)
    {
        return await alertRepository.FindAllByBatchCodeAsNoTrackingAsync(query.BatchCode, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Alert?> Handle(GetAlertByIdQuery query, CancellationToken cancellationToken)
    {
        return await alertRepository.FindByIdAsNoTrackingAsync(query.AlertId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Alert?> Handle(GetAlertByCodeQuery query, CancellationToken cancellationToken)
    {
        return await alertRepository.FindByCodeAsNoTrackingAsync(query.AlertCode, cancellationToken);
    }
}
