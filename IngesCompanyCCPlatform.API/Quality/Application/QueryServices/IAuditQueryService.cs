using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Represents the audit query service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IAuditQueryService
{
    Task<IEnumerable<Audit>> Handle(GetAllAuditsQuery query, CancellationToken cancellationToken);

    Task<Audit?> Handle(GetAuditByIdQuery query, CancellationToken cancellationToken);
}
