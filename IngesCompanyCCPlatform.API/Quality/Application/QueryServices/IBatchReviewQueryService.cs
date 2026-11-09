using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Quality.Application.QueryServices;

/// <summary>
///     Represents the batch review query service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IBatchReviewQueryService
{
    Task<IEnumerable<BatchReview>> Handle(GetAllBatchReviewsQuery query, CancellationToken cancellationToken);

    Task<BatchReview?> Handle(GetBatchReviewByIdQuery query, CancellationToken cancellationToken);

    Task<BatchReview?> Handle(GetBatchReviewByProductionBatchIdQuery query, CancellationToken cancellationToken);
}
