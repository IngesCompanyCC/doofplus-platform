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
///     REST controller of the collaboration tasks of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Collaboration Tasks")]
public class CollaborationTasksController(
    ICollaborationTaskCommandService collaborationTaskCommandService,
    ICollaborationTaskQueryService collaborationTaskQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a collaboration task")]
    [EndpointDescription("Creates a collaboration task.")]
    [ProducesResponseType(typeof(CollaborationTaskResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCollaborationTask([FromBody] CreateCollaborationTaskResource resource, CancellationToken cancellationToken)
    {
        var command = CollaborationTaskCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await collaborationTaskCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetCollaborationTaskById), new { collaborationTaskId = created.Id.Value },
                CollaborationTaskResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the collaboration tasks")]
    [EndpointDescription("Gets the collaboration tasks; with recordCode, only the ones with that record code.")]
    [ProducesResponseType(typeof(IEnumerable<CollaborationTaskResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCollaborationTasks([FromQuery] string? recordCode, CancellationToken cancellationToken)
    {
        var items = await collaborationTaskQueryService.Handle(new GetAllCollaborationTasksQuery(recordCode), cancellationToken);
        return Ok(items.Select(CollaborationTaskResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{collaborationTaskId:int}")]
    [EndpointSummary("Get a collaboration task by its id")]
    [EndpointDescription("Gets a collaboration task by the provided id.")]
    [ProducesResponseType(typeof(CollaborationTaskResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCollaborationTaskById([FromRoute] int collaborationTaskId, CancellationToken cancellationToken)
    {
        var item = await collaborationTaskQueryService.Handle(new GetCollaborationTaskByIdQuery(collaborationTaskId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, CollaborationTaskErrors.NotFoundById(collaborationTaskId),
            QualityError.CollaborationTaskNotFound, messages, problemDetailsFactory,
            found => Ok(CollaborationTaskResourceFromEntityAssembler.ToResourceFromEntity(found)), collaborationTaskId);
    }
}
