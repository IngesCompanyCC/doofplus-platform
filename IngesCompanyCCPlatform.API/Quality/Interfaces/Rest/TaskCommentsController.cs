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
///     REST controller of the task comments of Quality &amp; Compliance.
/// </summary>
[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Task Comments")]
public class TaskCommentsController(
    ITaskCommentCommandService taskCommentCommandService,
    ITaskCommentQueryService taskCommentQueryService,
    IStringLocalizer<QualityMessages> messages,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a task comment")]
    [EndpointDescription("Creates a task comment.")]
    [ProducesResponseType(typeof(TaskCommentResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTaskComment([FromBody] CreateTaskCommentResource resource, CancellationToken cancellationToken)
    {
        var command = TaskCommentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await taskCommentCommandService.Handle(command, cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromCommandResult(this, result, messages, problemDetailsFactory,
            created => CreatedAtAction(nameof(GetTaskCommentById), new { taskCommentId = created.Id.Value },
                TaskCommentResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpGet]
    [EndpointSummary("Get the task comments")]
    [EndpointDescription("Gets the task comments; with recordCode, only the ones with that record code.")]
    [ProducesResponseType(typeof(IEnumerable<TaskCommentResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskComments([FromQuery] string? recordCode, CancellationToken cancellationToken)
    {
        var items = await taskCommentQueryService.Handle(new GetAllTaskCommentsQuery(recordCode), cancellationToken);
        return Ok(items.Select(TaskCommentResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{taskCommentId:int}")]
    [EndpointSummary("Get a task comment by its id")]
    [EndpointDescription("Gets a task comment by the provided id.")]
    [ProducesResponseType(typeof(TaskCommentResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTaskCommentById([FromRoute] int taskCommentId, CancellationToken cancellationToken)
    {
        var item = await taskCommentQueryService.Handle(new GetTaskCommentByIdQuery(taskCommentId), cancellationToken);
        return QualityActionResultAssembler.ToActionResultFromQueryResult(this, item, TaskCommentErrors.NotFoundById(taskCommentId),
            QualityError.TaskCommentNotFound, messages, problemDetailsFactory,
            found => Ok(TaskCommentResourceFromEntityAssembler.ToResourceFromEntity(found)), taskCommentId);
    }
}
