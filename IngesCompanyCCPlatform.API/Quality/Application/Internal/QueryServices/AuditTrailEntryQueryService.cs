using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Implements the audit trail query service of the Quality &amp; Compliance bounded context.
/// </summary>
public class AuditTrailEntryQueryService(IAuditTrailEntryRepository auditTrailEntryRepository)
    : IAuditTrailEntryQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<AuditTrailEntry>> Handle(GetAllAuditTrailEntriesQuery query,
        CancellationToken cancellationToken)
    {
        var entries = await auditTrailEntryRepository.ListAsNoTrackingAsync(cancellationToken);
        return entries.OrderByDescending(entry => entry.ChangedAt);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AuditTrailEntry>> Handle(GetAuditTrailEntriesByEntityQuery query,
        CancellationToken cancellationToken)
    {
        return await auditTrailEntryRepository.FindByEntityAsync(query.EntityType, query.EntityId, cancellationToken);
    }
}
