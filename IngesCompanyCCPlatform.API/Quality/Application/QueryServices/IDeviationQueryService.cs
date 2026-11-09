using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Represents the deviation query service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IDeviationQueryService
{
    Task<IEnumerable<Deviation>> Handle(GetAllDeviationsQuery query, CancellationToken cancellationToken);

    Task<Deviation?> Handle(GetDeviationByIdQuery query, CancellationToken cancellationToken);

    Task<IEnumerable<Deviation>> Handle(GetDeviationsByProductionBatchIdQuery query,
        CancellationToken cancellationToken);
}
