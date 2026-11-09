using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Acl;

namespace IngesCompanyCCPlatform.API.Quality.Application.Acl;

/// <summary>
///     Implements the facade of the Quality &amp; Compliance bounded context.
/// </summary>
public class QualityContextFacade(
    IMaterialApprovalCommandService materialApprovalCommandService,
    IBatchReviewCommandService batchReviewCommandService,
    IBatchReviewQueryService batchReviewQueryService) : IQualityContextFacade
{
    /// <inheritdoc />
    public async Task<int> RequestMaterialApproval(int rawMaterialLotId, CancellationToken cancellationToken)
    {
        var command = new RequestMaterialApprovalCommand(new RawMaterialLotId(rawMaterialLotId));
        var result = await materialApprovalCommandService.Handle(command, cancellationToken);
        return result.IsSuccess ? result.Value!.Id : 0;
    }

    /// <inheritdoc />
    public async Task<int> StartBatchQuarantine(int productionBatchId, CancellationToken cancellationToken)
    {
        var command = new StartQuarantineCommand(new ProductionBatchId(productionBatchId));
        var result = await batchReviewCommandService.Handle(command, cancellationToken);
        return result.IsSuccess ? result.Value!.Id : 0;
    }

    /// <inheritdoc />
    public async Task<string> FetchBatchReviewStatus(int productionBatchId, CancellationToken cancellationToken)
    {
        var query = new GetBatchReviewByProductionBatchIdQuery(new ProductionBatchId(productionBatchId));
        var batchReview = await batchReviewQueryService.Handle(query, cancellationToken);
        return batchReview?.Status.ToString() ?? string.Empty;
    }
}
