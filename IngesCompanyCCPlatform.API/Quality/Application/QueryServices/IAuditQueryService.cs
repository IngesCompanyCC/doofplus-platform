using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="Audit" />.
/// </summary>
public interface IAuditQueryService
{
    /// <summary>
    ///     Gets the audits.
    /// </summary>
    Task<IEnumerable<Audit>> Handle(GetAllAuditsQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a audit by its id, or <c>null</c>.
    /// </summary>
    Task<Audit?> Handle(GetAuditByIdQuery query, CancellationToken cancellationToken);
}
