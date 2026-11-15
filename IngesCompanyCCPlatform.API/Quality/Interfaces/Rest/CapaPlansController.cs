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
///     REST controller of the capa plans of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Capa Plans")]
public class CapaPlansController(
    ICapaPlanCommandService capaPlanCommandService,
    ICapaPlanQueryService capaPlanQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a capa plan")]
    [EndpointDescription("Creates a capa plan.")]
    [ProducesResponseType(typeof(CapaPlanResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateCapaPlan([FromBody] CreateCapaPlanResource resource, CancellationToken cancellationToken)
    {
        var command = CapaPlanCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await capaPlanCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetCapaPlanById), new { capaPlanId = created.Id.Value },
                CapaPlanResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the capa plans")]
    [EndpointDescription("Gets the capa plans; with sourceDeviation, only the ones with that source deviation.")]
    [ProducesResponseType(typeof(IEnumerable<CapaPlanResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCapaPlans([FromQuery] string? sourceDeviation, CancellationToken cancellationToken)
    {
        var items = await capaPlanQueryService.Handle(new GetAllCapaPlansQuery(sourceDeviation), cancellationToken);
        return Ok(items.Select(CapaPlanResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{capaPlanId:int}")]
    [EndpointSummary("Get a capa plan by its id")]
    [EndpointDescription("Gets a capa plan by the provided id.")]
    [ProducesResponseType(typeof(CapaPlanResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCapaPlanById([FromRoute] int capaPlanId, CancellationToken cancellationToken)
    {
        var item = await capaPlanQueryService.Handle(new GetCapaPlanByIdQuery(capaPlanId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, CapaPlanErrors.NotFoundById(capaPlanId),
            QualityError.CapaPlanNotFound, messages, problemDetailsFactory,
            found => Ok(CapaPlanResourceFromEntityAssembler.ToResourceFromEntity(found)), capaPlanId);
    }

    [HttpPut("{capaPlanId:int}")]
    [EndpointSummary("Update a capa plan")]
    [EndpointDescription("Replaces the data of the capa plan with the provided id.")]
    [ProducesResponseType(typeof(CapaPlanResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCapaPlan([FromRoute] int capaPlanId, [FromBody] UpdateCapaPlanResource resource, CancellationToken cancellationToken)
    {
        var command = CapaPlanCommandFromResourceAssembler.ToCommandFromResource(capaPlanId, resource);
        var result = await capaPlanCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            updated => Ok(CapaPlanResourceFromEntityAssembler.ToResourceFromEntity(updated)), capaPlanId);
    }
}
