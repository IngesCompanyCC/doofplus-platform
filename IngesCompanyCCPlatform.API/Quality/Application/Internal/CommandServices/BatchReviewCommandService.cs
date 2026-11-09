using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Implements the batch review command service of the Quality &amp; Compliance bounded context.
/// </summary>
public class BatchReviewCommandService(
    IBatchReviewRepository batchReviewRepository,
    IAnalyticalResultRepository analyticalResultRepository,
    IDeviationRepository deviationRepository,
    IUnitOfWork unitOfWork) : IBatchReviewCommandService
{
    /// <inheritdoc />
    public async Task<Result<BatchReview>> Handle(StartQuarantineCommand command, CancellationToken cancellationToken)
    {
        if (await batchReviewRepository.FindByProductionBatchIdAsync(command.ProductionBatchId,
                cancellationToken) is not null)
            return Result<BatchReview>.Failure(QualityErrors.BatchReviewAlreadyStarted(command.ProductionBatchId),
                QualityError.BatchReviewAlreadyStarted);

        var batchReview = new BatchReview(command);
        await batchReviewRepository.AddAsync(batchReview, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<BatchReview>.Success(batchReview);
    }

    /// <inheritdoc />
    public Task<Result<BatchReview>> Handle(RegisterBatchSampleCommand command, CancellationToken cancellationToken)
    {
        return Update(command.BatchReviewId, review => review.RegisterSample(command.SampleCode), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<BatchReview>> Handle(AttachBatchEvidenceCommand command, CancellationToken cancellationToken)
    {
        return Update(command.BatchReviewId, review => review.AttachEvidence(command.FileUrl, command.Description),
            cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Result<BatchReview>> Handle(EvaluateBatchCommand command, CancellationToken cancellationToken)
    {
        var batchReview = await batchReviewRepository.FindByIdAsync(command.BatchReviewId, cancellationToken);
        if (batchReview is null) return NotFound(command.BatchReviewId);

        var results = await analyticalResultRepository.FindByProductionBatchIdAsync(batchReview.ProductionBatchId,
            cancellationToken);
        var deviations = await deviationRepository.FindByProductionBatchIdAsync(batchReview.ProductionBatchId,
            cancellationToken);
        var hasOpenDeviations = deviations.Any(deviation => deviation.Status != EDeviationStatus.Closed);
        var hasClosedDeviations = deviations.Any(deviation => deviation.Status == EDeviationStatus.Closed);

        var result = QualityDomainRules.Apply(batchReview,
            review => review.Evaluate(results, hasOpenDeviations, hasClosedDeviations));
        if (result.IsFailure) return result;

        await unitOfWork.CompleteAsync(cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public Task<Result<BatchReview>> Handle(ReleaseBatchCommand command, CancellationToken cancellationToken)
    {
        return Update(command.BatchReviewId, review => review.Release(command.SignatureId), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<BatchReview>> Handle(RejectBatchCommand command, CancellationToken cancellationToken)
    {
        return Update(command.BatchReviewId, review => review.Reject(command.Reason, command.SignatureId),
            cancellationToken);
    }

    private async Task<Result<BatchReview>> Update(BatchReviewId id, Action<BatchReview> behavior,
        CancellationToken cancellationToken)
    {
        var batchReview = await batchReviewRepository.FindByIdAsync(id, cancellationToken);
        if (batchReview is null) return NotFound(id);

        var result = QualityDomainRules.Apply(batchReview, behavior);
        if (result.IsFailure) return result;

        await unitOfWork.CompleteAsync(cancellationToken);
        return result;
    }

    private static Result<BatchReview> NotFound(BatchReviewId id)
    {
        return Result<BatchReview>.Failure(QualityErrors.BatchReviewNotFound(id), QualityError.BatchReviewNotFound);
    }
}
