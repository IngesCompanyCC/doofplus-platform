using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Implements the deviation query service of the Quality &amp; Compliance bounded context.
/// </summary>
public class DeviationQueryService(IDeviationRepository deviationRepository) : IDeviationQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Deviation>> Handle(GetAllDeviationsQuery query, CancellationToken cancellationToken)
    {
        return await deviationRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Deviation?> Handle(GetDeviationByIdQuery query, CancellationToken cancellationToken)
    {
        return await deviationRepository.FindByIdAsNoTrackingAsync(query.DeviationId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Deviation>> Handle(GetDeviationsByProductionBatchIdQuery query,
        CancellationToken cancellationToken)
    {
        return await deviationRepository.FindByProductionBatchIdAsync(query.ProductionBatchId, cancellationToken);
    }
}
