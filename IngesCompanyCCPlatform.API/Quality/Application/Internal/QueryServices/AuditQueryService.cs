using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Implements the audit query service of the Quality &amp; Compliance bounded context.
/// </summary>
public class AuditQueryService(IAuditRepository auditRepository) : IAuditQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Audit>> Handle(GetAllAuditsQuery query, CancellationToken cancellationToken)
    {
        return await auditRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Audit?> Handle(GetAuditByIdQuery query, CancellationToken cancellationToken)
    {
        return await auditRepository.FindByIdAsNoTrackingAsync(query.AuditId, cancellationToken);
    }
}
