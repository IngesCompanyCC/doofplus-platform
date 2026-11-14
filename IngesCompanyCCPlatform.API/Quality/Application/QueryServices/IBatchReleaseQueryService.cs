using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Query service of <see cref="BatchRelease" />.
/// </summary>
public interface IBatchReleaseQueryService
{
    /// <summary>
    ///     Gets all the batch releases.
    /// </summary>
    Task<IEnumerable<BatchRelease>> Handle(GetAllBatchReleasesQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets a batch release by its id, or <c>null</c>.
    /// </summary>
    Task<BatchRelease?> Handle(GetBatchReleaseByIdQuery query, CancellationToken cancellationToken);
    /// <summary>
    ///     Gets the batch releases filtered by batch id.
    /// </summary>
    Task<IEnumerable<BatchRelease>> Handle(GetAllBatchReleasesByBatchIdQuery query, CancellationToken cancellationToken);
}
