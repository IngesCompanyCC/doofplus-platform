using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="AuditFinding" />.
/// </summary>
public interface IAuditFindingQueryService
{
    /// <summary>
    ///     Gets all the audit findings.
    /// </summary>
    Task<IEnumerable<AuditFinding>> Handle(GetAllAuditFindingsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a audit finding by its id, or <c>null</c>.
    /// </summary>
    Task<AuditFinding?> Handle(GetAuditFindingByIdQuery query, CancellationToken cancellationToken);
    /// <summary>
    ///     Gets the audit findings filtered by audit id.
    /// </summary>
    Task<IEnumerable<AuditFinding>> Handle(GetAllAuditFindingsByAuditIdQuery query, CancellationToken cancellationToken);
}
