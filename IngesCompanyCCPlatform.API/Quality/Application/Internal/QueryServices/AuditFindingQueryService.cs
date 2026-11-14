using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="AuditFinding" />.
/// </summary>
public class AuditFindingQueryService(IAuditFindingRepository auditFindingRepository) : IAuditFindingQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<AuditFinding>> Handle(GetAllAuditFindingsQuery query, CancellationToken cancellationToken)
    {
        return await auditFindingRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AuditFinding?> Handle(GetAuditFindingByIdQuery query, CancellationToken cancellationToken)
    {
        return await auditFindingRepository.FindByIdAsNoTrackingAsync(query.AuditFindingId, cancellationToken);
    }
    /// <inheritdoc />
    public async Task<IEnumerable<AuditFinding>> Handle(GetAllAuditFindingsByAuditIdQuery query, CancellationToken cancellationToken)
    {
        return await auditFindingRepository.FindAllByAuditIdAsNoTrackingAsync(query.AuditId, cancellationToken);
    }
}
