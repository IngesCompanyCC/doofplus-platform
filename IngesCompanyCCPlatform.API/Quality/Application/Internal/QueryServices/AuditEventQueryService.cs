using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="AuditEvent" />.
/// </summary>
public class AuditEventQueryService(IAuditEventRepository auditEventRepository) : IAuditEventQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<AuditEvent>> Handle(GetAllAuditEventsQuery query, CancellationToken cancellationToken)
    {
        return await auditEventRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AuditEvent?> Handle(GetAuditEventByIdQuery query, CancellationToken cancellationToken)
    {
        return await auditEventRepository.FindByIdAsNoTrackingAsync(query.AuditEventId, cancellationToken);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<AuditEvent>> Handle(GetAllAuditEventsByAggregateTypeAggregateIdQuery query, CancellationToken cancellationToken)
    {
        return await auditEventRepository.FindAllByAggregateTypeAggregateIdAsNoTrackingAsync(query.AggregateType, query.AggregateId, cancellationToken);
    }
}
