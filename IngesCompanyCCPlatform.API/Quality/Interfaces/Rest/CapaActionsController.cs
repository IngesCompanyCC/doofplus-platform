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
using IngesCompanyCCPlatform.API.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest;

/// <summary>
///     REST controller of the capa actions of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Authorize]
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
    public async Task<IActionResult> CreateCapaAction([FromBody] CreateCapaActionResource resource, CancellationToken cancellationToken)
    {
        var command = CapaActionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await capaActionCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetCapaActionById), new { capaActionId = created.Id.Value },
                CapaActionResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the capa actions")]
    [EndpointDescription("Gets the capa actions; with capaCode, only the ones with that capa code.")]
    [ProducesResponseType(typeof(IEnumerable<CapaActionResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCapaActions([FromQuery] string? capaCode, CancellationToken cancellationToken)
    {
        var items = await capaActionQueryService.Handle(new GetAllCapaActionsQuery(capaCode), cancellationToken);
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

    [HttpPut("{capaActionId:int}")]
    [EndpointSummary("Update a capa action")]
    [EndpointDescription("Replaces the data of the capa action with the provided id.")]
    [ProducesResponseType(typeof(CapaActionResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCapaAction([FromRoute] int capaActionId, [FromBody] UpdateCapaActionResource resource, CancellationToken cancellationToken)
    {
        var command = CapaActionCommandFromResourceAssembler.ToCommandFromResource(capaActionId, resource);
        var result = await capaActionCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            updated => Ok(CapaActionResourceFromEntityAssembler.ToResourceFromEntity(updated)), capaActionId);
    }
}
