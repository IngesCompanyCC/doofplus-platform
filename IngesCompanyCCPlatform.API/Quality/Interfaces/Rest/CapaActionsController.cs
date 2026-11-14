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
///     REST controller of the capa actions of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Capa Actions")]
public class CapaActionsController(
    ICapaActionCommandService capaActionCommandService,
    ICapaActionQueryService capaActionQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a capa action")]
    [EndpointDescription("Creates a capa action.")]
    [ProducesResponseType(typeof(CapaActionResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateCapaAction([FromBody] CreateCapaActionResource resource, CancellationToken cancellationToken)
    {
        var command = CapaActionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await capaActionCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetCapaActionById), new { capaActionId = created.Id.Value },
                CapaActionResourceFromEntityAssembler.ToResourceFromEntity(created)), resource.CapaPlanId);
    }
    [HttpGet]
    [EndpointSummary("Get the capa actions")]
    [EndpointDescription("Gets all the capa actions.")]
    [ProducesResponseType(typeof(IEnumerable<CapaActionResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCapaActions(CancellationToken cancellationToken)
    {
        var items = await capaActionQueryService.Handle(new GetAllCapaActionsQuery(), cancellationToken);
        return Ok(items.Select(CapaActionResourceFromEntityAssembler.ToResourceFromEntity));
    }
    [HttpGet("{capaActionId:int}")]
    [EndpointSummary("Get a capa action by its id")]
    [EndpointDescription("Gets a capa action by the provided id.")]
    [ProducesResponseType(typeof(CapaActionResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCapaActionById([FromRoute] int capaActionId, CancellationToken cancellationToken)
    {
        var item = await capaActionQueryService.Handle(new GetCapaActionByIdQuery(capaActionId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, CapaActionErrors.NotFoundById(capaActionId),
            QualityError.CapaActionNotFound, messages, problemDetailsFactory,
            found => Ok(CapaActionResourceFromEntityAssembler.ToResourceFromEntity(found)), capaActionId);
    }
    [HttpGet("by-plan/{capaPlanId:int}")]
    [EndpointSummary("Get the capa actions of a capa plan")]
    [EndpointDescription("Gets the capa actions of the capa plan with the provided id.")]
    [ProducesResponseType(typeof(IEnumerable<CapaActionResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCapaActionsByCapaPlanId([FromRoute] int capaPlanId, CancellationToken cancellationToken)
    {
        var items = await capaActionQueryService.Handle(new GetAllCapaActionsByCapaPlanIdQuery(capaPlanId), cancellationToken);
        return Ok(items.Select(CapaActionResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
