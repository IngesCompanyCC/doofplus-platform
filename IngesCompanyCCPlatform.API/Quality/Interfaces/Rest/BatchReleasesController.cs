using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;
using IngesCompanyCCPlatform.API.Quality.Resources;
using IngesCompanyCCPlatform.API.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

/// <summary>
///     REST controller of the batch releases of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Batch Releases")]
public class BatchReleasesController(
    IBatchReleaseCommandService batchReleaseCommandService,
    IBatchReleaseQueryService batchReleaseQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a batch release")]
    [EndpointDescription("Creates a batch release.")]
    [ProducesResponseType(typeof(BatchReleaseResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateBatchRelease([FromBody] CreateBatchReleaseResource resource, CancellationToken cancellationToken)
    {
        var command = BatchReleaseCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await batchReleaseCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetBatchReleaseById), new { batchReleaseId = created.Id.Value },
                BatchReleaseResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }
    [HttpGet]
    [EndpointSummary("Get the batch releases")]
    [EndpointDescription("Gets all the batch releases.")]
    [ProducesResponseType(typeof(IEnumerable<BatchReleaseResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBatchReleases(CancellationToken cancellationToken)
    {
        var items = await batchReleaseQueryService.Handle(new GetAllBatchReleasesQuery(), cancellationToken);
        return Ok(items.Select(BatchReleaseResourceFromEntityAssembler.ToResourceFromEntity));
    }
    [HttpGet("{batchReleaseId:int}")]
    [EndpointSummary("Get a batch release by its id")]
    [EndpointDescription("Gets a batch release by the provided id.")]
    [ProducesResponseType(typeof(BatchReleaseResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBatchReleaseById([FromRoute] int batchReleaseId, CancellationToken cancellationToken)
    {
        var item = await batchReleaseQueryService.Handle(new GetBatchReleaseByIdQuery(batchReleaseId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, BatchReleaseErrors.NotFoundById(batchReleaseId),
            QualityError.BatchReleaseNotFound, messages, problemDetailsFactory,
            found => Ok(BatchReleaseResourceFromEntityAssembler.ToResourceFromEntity(found)), batchReleaseId);
    }
    [HttpGet("by-batch/{batchId:int}")]
    [EndpointSummary("Get the batch releases of a batch")]
    [EndpointDescription("Gets the batch releases of the batch with the provided id.")]
    [ProducesResponseType(typeof(IEnumerable<BatchReleaseResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBatchReleasesByBatchId([FromRoute] int batchId, CancellationToken cancellationToken)
    {
        var items = await batchReleaseQueryService.Handle(new GetAllBatchReleasesByBatchIdQuery(batchId), cancellationToken);
        return Ok(items.Select(BatchReleaseResourceFromEntityAssembler.ToResourceFromEntity));
    }
    [HttpPut("{batchReleaseId:int}/approve")]
    [EndpointSummary("Approve a batch release")]
    [EndpointDescription("Approves a pending release with the electronic signature of Quality. Answers 409 when the release is not pending.")]
    [ProducesResponseType(typeof(BatchReleaseResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ApproveBatchRelease([FromRoute] int batchReleaseId, [FromBody] ApproveBatchReleaseResource resource,
        CancellationToken cancellationToken)
    {
        var command = BatchReleaseCommandFromResourceAssembler.ToCommandFromResource(batchReleaseId, resource);
        var result = await batchReleaseCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            updated => Ok(BatchReleaseResourceFromEntityAssembler.ToResourceFromEntity(updated)), batchReleaseId);
    }
    [HttpPut("{batchReleaseId:int}/reject")]
    [EndpointSummary("Reject a batch release")]
    [EndpointDescription("Rejects a pending release with its reason. Answers 409 when the release is not pending.")]
    [ProducesResponseType(typeof(BatchReleaseResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RejectBatchRelease([FromRoute] int batchReleaseId, [FromBody] RejectBatchReleaseResource resource,
        CancellationToken cancellationToken)
    {
        var command = BatchReleaseCommandFromResourceAssembler.ToCommandFromResource(batchReleaseId, resource);
        var result = await batchReleaseCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            updated => Ok(BatchReleaseResourceFromEntityAssembler.ToResourceFromEntity(updated)), batchReleaseId);
    }
}
