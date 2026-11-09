using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Represents the audit trail query service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IAuditTrailEntryQueryService
{
    Task<IEnumerable<AuditTrailEntry>> Handle(GetAllAuditTrailEntriesQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<AuditTrailEntry>> Handle(GetAuditTrailEntriesByEntityQuery query,
        CancellationToken cancellationToken);
}
