using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="Deviation" />.
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
    public async Task<IEnumerable<Deviation>> Handle(GetAllDeviationsByBatchIdQuery query, CancellationToken cancellationToken)
    {
        return await deviationRepository.FindAllByBatchIdAsNoTrackingAsync(query.BatchId, cancellationToken);
    }
}
