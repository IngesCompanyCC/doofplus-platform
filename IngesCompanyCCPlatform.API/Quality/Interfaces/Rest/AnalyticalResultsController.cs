using System.Net.Mime;
using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Application.QueryServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
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
///     REST controller of the analytical results of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Analytical Results")]
public class AnalyticalResultsController(
    IAnalyticalResultCommandService analyticalResultCommandService,
    IAnalyticalResultQueryService analyticalResultQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a analytical result")]
    [EndpointDescription("Creates a analytical result.")]
    [ProducesResponseType(typeof(AnalyticalResultResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAnalyticalResult([FromBody] CreateAnalyticalResultResource resource, CancellationToken cancellationToken)
    {
        var command = AnalyticalResultCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await analyticalResultCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetAnalyticalResultById), new { analyticalResultId = created.Id.Value },
                AnalyticalResultResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the analytical results")]
    [EndpointDescription("Gets the analytical results; with batchCode, only the ones with that batch code.")]
    [ProducesResponseType(typeof(IEnumerable<AnalyticalResultResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAnalyticalResults([FromQuery] string? batchCode, CancellationToken cancellationToken)
    {
        var items = await analyticalResultQueryService.Handle(new GetAllAnalyticalResultsQuery(batchCode), cancellationToken);
        return Ok(items.Select(AnalyticalResultResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{analyticalResultId:int}")]
    [EndpointSummary("Get a analytical result by its id")]
    [EndpointDescription("Gets a analytical result by the provided id.")]
    [ProducesResponseType(typeof(AnalyticalResultResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAnalyticalResultById([FromRoute] int analyticalResultId, CancellationToken cancellationToken)
    {
        var item = await analyticalResultQueryService.Handle(new GetAnalyticalResultByIdQuery(analyticalResultId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, AnalyticalResultErrors.NotFoundById(analyticalResultId),
            QualityError.AnalyticalResultNotFound, messages, problemDetailsFactory,
            found => Ok(AnalyticalResultResourceFromEntityAssembler.ToResourceFromEntity(found)), analyticalResultId);
    }

    [HttpPut("{analyticalResultId:int}")]
    [EndpointSummary("Update a analytical result")]
    [EndpointDescription("Replaces the data of the analytical result with the provided id.")]
    [ProducesResponseType(typeof(AnalyticalResultResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAnalyticalResult([FromRoute] int analyticalResultId, [FromBody] UpdateAnalyticalResultResource resource, CancellationToken cancellationToken)
    {
        var command = AnalyticalResultCommandFromResourceAssembler.ToCommandFromResource(analyticalResultId, resource);
        var result = await analyticalResultCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            updated => Ok(AnalyticalResultResourceFromEntityAssembler.ToResourceFromEntity(updated)), analyticalResultId);
    }
}
