using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Represents the batch review command service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IBatchReviewCommandService
{
    /// <summary>Puts a production batch in quarantine and opens its review.</summary>
    Task<Result<BatchReview>> Handle(StartQuarantineCommand command, CancellationToken cancellationToken);

    /// <summary>Registers the sample of a batch in quarantine.</summary>
    Task<Result<BatchReview>> Handle(RegisterBatchSampleCommand command, CancellationToken cancellationToken);

    /// <summary>Attaches an evidence file to the review.</summary>
    Task<Result<BatchReview>> Handle(AttachBatchEvidenceCommand command, CancellationToken cancellationToken);

    /// <summary>Evaluates the batch with its analytical results and deviations.</summary>
    Task<Result<BatchReview>> Handle(EvaluateBatchCommand command, CancellationToken cancellationToken);

    /// <summary>Releases an evaluated batch and issues its certificate.</summary>
    Task<Result<BatchReview>> Handle(ReleaseBatchCommand command, CancellationToken cancellationToken);

    /// <summary>Rejects a batch under review and issues its certificate.</summary>
    Task<Result<BatchReview>> Handle(RejectBatchCommand command, CancellationToken cancellationToken);
}
