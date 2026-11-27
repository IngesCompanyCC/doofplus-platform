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
    [EndpointDescription("Gets all the batch releases, or only those of a batch when batchId is given.")]
    [ProducesResponseType(typeof(IEnumerable<BatchReleaseResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBatchReleases([FromQuery] int? batchId, CancellationToken cancellationToken)
    {
        var items = batchId is { } id
            ? await batchReleaseQueryService.Handle(new GetAllBatchReleasesByBatchIdQuery(id), cancellationToken)
            : await batchReleaseQueryService.Handle(new GetAllBatchReleasesQuery(), cancellationToken);
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
    [HttpPost("{batchReleaseId:int}/approvals")]
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
    [HttpPost("{batchReleaseId:int}/rejections")]
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
