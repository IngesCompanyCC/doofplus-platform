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
///     REST controller of the deviations of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Deviations")]
public class DeviationsController(
    IDeviationCommandService deviationCommandService,
    IDeviationQueryService deviationQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a deviation")]
    [EndpointDescription("Creates a deviation.")]
    [ProducesResponseType(typeof(DeviationResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateDeviation([FromBody] CreateDeviationResource resource, CancellationToken cancellationToken)
    {
        var command = DeviationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await deviationCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetDeviationById), new { deviationId = created.Id.Value },
                DeviationResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the deviations")]
    [EndpointDescription("Gets the deviations; with batchCode, only the ones with that batch code.")]
    [ProducesResponseType(typeof(IEnumerable<DeviationResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDeviations([FromQuery] string? batchCode, CancellationToken cancellationToken)
    {
        var items = await deviationQueryService.Handle(new GetAllDeviationsQuery(batchCode), cancellationToken);
        return Ok(items.Select(DeviationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{deviationId:int}")]
    [EndpointSummary("Get a deviation by its id")]
    [EndpointDescription("Gets a deviation by the provided id.")]
    [ProducesResponseType(typeof(DeviationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDeviationById([FromRoute] int deviationId, CancellationToken cancellationToken)
    {
        var item = await deviationQueryService.Handle(new GetDeviationByIdQuery(deviationId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, DeviationErrors.NotFoundById(deviationId),
            QualityError.DeviationNotFound, messages, problemDetailsFactory,
            found => Ok(DeviationResourceFromEntityAssembler.ToResourceFromEntity(found)), deviationId);
    }

    [HttpPut("{deviationId:int}")]
    [EndpointSummary("Update a deviation")]
    [EndpointDescription("Replaces the data of the deviation with the provided id.")]
    [ProducesResponseType(typeof(DeviationResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDeviation([FromRoute] int deviationId, [FromBody] UpdateDeviationResource resource, CancellationToken cancellationToken)
    {
        var command = DeviationCommandFromResourceAssembler.ToCommandFromResource(deviationId, resource);
        var result = await deviationCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            updated => Ok(DeviationResourceFromEntityAssembler.ToResourceFromEntity(updated)), deviationId);
    }
}
