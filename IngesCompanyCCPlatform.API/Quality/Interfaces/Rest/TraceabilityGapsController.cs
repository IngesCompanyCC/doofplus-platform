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

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

/// <summary>
///     REST controller of the traceability gaps of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Traceability Gaps")]
public class TraceabilityGapsController(
    ITraceabilityGapCommandService traceabilityGapCommandService,
    ITraceabilityGapQueryService traceabilityGapQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a traceability gap")]
    [EndpointDescription("Creates a traceability gap.")]
    [ProducesResponseType(typeof(TraceabilityGapResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateTraceabilityGap([FromBody] CreateTraceabilityGapResource resource, CancellationToken cancellationToken)
    {
        var command = TraceabilityGapCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await traceabilityGapCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetTraceabilityGapById), new { traceabilityGapId = created.Id.Value },
                TraceabilityGapResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }
    [HttpGet]
    [EndpointSummary("Get the traceability gaps")]
    [EndpointDescription("Gets all the traceability gaps.")]
    [ProducesResponseType(typeof(IEnumerable<TraceabilityGapResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTraceabilityGaps(CancellationToken cancellationToken)
    {
        var items = await traceabilityGapQueryService.Handle(new GetAllTraceabilityGapsQuery(), cancellationToken);
        return Ok(items.Select(TraceabilityGapResourceFromEntityAssembler.ToResourceFromEntity));
    }
    [HttpGet("{traceabilityGapId:int}")]
    [EndpointSummary("Get a traceability gap by its id")]
    [EndpointDescription("Gets a traceability gap by the provided id.")]
    [ProducesResponseType(typeof(TraceabilityGapResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTraceabilityGapById([FromRoute] int traceabilityGapId, CancellationToken cancellationToken)
    {
        var item = await traceabilityGapQueryService.Handle(new GetTraceabilityGapByIdQuery(traceabilityGapId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, TraceabilityGapErrors.NotFoundById(traceabilityGapId),
            QualityError.TraceabilityGapNotFound, messages, problemDetailsFactory,
            found => Ok(TraceabilityGapResourceFromEntityAssembler.ToResourceFromEntity(found)), traceabilityGapId);
    }
    [HttpGet("by-batch/{batchId:int}")]
    [EndpointSummary("Get the traceability gaps of a batch")]
    [EndpointDescription("Gets the traceability gaps of the batch with the provided id.")]
    [ProducesResponseType(typeof(IEnumerable<TraceabilityGapResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTraceabilityGapsByBatchId([FromRoute] int batchId, CancellationToken cancellationToken)
    {
        var items = await traceabilityGapQueryService.Handle(new GetAllTraceabilityGapsByBatchIdQuery(batchId), cancellationToken);
        return Ok(items.Select(TraceabilityGapResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
