using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Filters;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Batch Reviews")]
[QualityInvalidDataFilter]
public class BatchReviewsController(
    IBatchReviewCommandService batchReviewCommandService,
    IBatchReviewQueryService batchReviewQueryService,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get the batch reviews")]
    [EndpointDescription("Gets all the batch reviews, or the one of a production batch with ?productionBatchId=.")]
    [ProducesResponseType(typeof(IEnumerable<BatchReviewResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBatchReviews([FromQuery] int? productionBatchId,
        CancellationToken cancellationToken)
    {
        if (productionBatchId is not null)
        {
            var review = await batchReviewQueryService.Handle(
                new GetBatchReviewByProductionBatchIdQuery(new ProductionBatchId(productionBatchId.Value)),
                cancellationToken);
            var reviews = review is null ? [] : new[] { review };
            return Ok(reviews.Select(BatchReviewResourceFromEntityAssembler.ToResourceFromEntity));
        }

        var all = await batchReviewQueryService.Handle(new GetAllBatchReviewsQuery(), cancellationToken);
        return Ok(all.Select(BatchReviewResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{batchReviewId:int}")]
    [EndpointSummary("Get a batch review by id")]
    [ProducesResponseType(typeof(BatchReviewResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBatchReviewById(int batchReviewId, CancellationToken cancellationToken)
    {
        var review = await batchReviewQueryService.Handle(new GetBatchReviewByIdQuery(batchReviewId),
            cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQuery(this, review, problemDetailsFactory,
            QualityError.BatchReviewNotFound, QualityErrors.BatchReviewNotFound(batchReviewId).Message,
            found => Ok(BatchReviewResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [EndpointSummary("Put a production batch in quarantine")]
    [ProducesResponseType(typeof(BatchReviewResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> StartQuarantine([FromBody] StartQuarantineResource resource,
        CancellationToken cancellationToken)
    {
        var command = BatchReviewCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await batchReviewCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetBatchReviewById), new { batchReviewId = created.Id.Value },
                BatchReviewResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpPost("{batchReviewId:int}/samples")]
    [EndpointSummary("Register the sample of a batch in quarantine")]
    [ProducesResponseType(typeof(BatchReviewResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterBatchSample(int batchReviewId,
        [FromBody] RegisterBatchSampleResource resource, CancellationToken cancellationToken)
    {
        var command = BatchReviewCommandFromResourceAssembler.ToCommandFromResource(batchReviewId, resource);
        return ToOk(await batchReviewCommandService.Handle(command, cancellationToken));
    }

    [HttpPost("{batchReviewId:int}/evidences")]
    [EndpointSummary("Attach an evidence to a batch review")]
    [ProducesResponseType(typeof(BatchReviewResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AttachBatchEvidence(int batchReviewId,
        [FromBody] AttachBatchEvidenceResource resource, CancellationToken cancellationToken)
    {
        var command = BatchReviewCommandFromResourceAssembler.ToCommandFromResource(batchReviewId, resource);
        return ToOk(await batchReviewCommandService.Handle(command, cancellationToken));
    }

    [HttpPost("{batchReviewId:int}/evaluation")]
    [EndpointSummary("Evaluate a batch")]
    [EndpointDescription(
        "Evaluates the batch with its analytical results and deviations. An open deviation, or an OOS result without a closed deviation, blocks the release.")]
    [ProducesResponseType(typeof(BatchReviewResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> EvaluateBatch(int batchReviewId, CancellationToken cancellationToken)
    {
        return ToOk(await batchReviewCommandService.Handle(new EvaluateBatchCommand(batchReviewId),
            cancellationToken));
    }

    [HttpPost("{batchReviewId:int}/release")]
    [EndpointSummary("Release an evaluated batch")]
    [ProducesResponseType(typeof(BatchReviewResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ReleaseBatch(int batchReviewId, [FromBody] ReleaseBatchResource resource,
        CancellationToken cancellationToken)
    {
        var command = BatchReviewCommandFromResourceAssembler.ToCommandFromResource(batchReviewId, resource);
        return ToOk(await batchReviewCommandService.Handle(command, cancellationToken));
    }

    [HttpPost("{batchReviewId:int}/rejection")]
    [EndpointSummary("Reject a batch under review")]
    [ProducesResponseType(typeof(BatchReviewResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RejectBatch(int batchReviewId, [FromBody] RejectBatchResource resource,
        CancellationToken cancellationToken)
    {
        var command = BatchReviewCommandFromResourceAssembler.ToCommandFromResource(batchReviewId, resource);
        return ToOk(await batchReviewCommandService.Handle(command, cancellationToken));
    }

    private IActionResult ToOk(Result<BatchReview> result)
    {
        return QualityActionResultAssembler.ToActionResultFromResult(this, result, problemDetailsFactory,
            review => Ok(BatchReviewResourceFromEntityAssembler.ToResourceFromEntity(review)));
    }
}
