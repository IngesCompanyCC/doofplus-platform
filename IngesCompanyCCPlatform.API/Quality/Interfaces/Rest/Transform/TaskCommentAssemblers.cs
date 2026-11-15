using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>TaskComment</c> from the resources. Enums are accepted in any case and with hyphens.
/// </summary>
public static class TaskCommentCommandFromResourceAssembler
{
    /// <summary>
    ///     Builds the command to create a task comment.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateTaskCommentCommand ToCommandFromResource(CreateTaskCommentResource resource)
    {
        return new CreateTaskCommentCommand(
            resource.RecordCode ?? string.Empty,
            resource.Author ?? string.Empty,
            resource.Initials ?? string.Empty,
            resource.PostedAt,
            resource.Text ?? string.Empty);
    }
}

/// <summary>
///     Builds the resource of a task comment.
/// </summary>
public static class TaskCommentResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The task comment.</param>
    /// <returns>The resource.</returns>
    public static TaskCommentResource ToResourceFromEntity(TaskComment entity)
    {
        return new TaskCommentResource(
            entity.Id.Value,
            entity.RecordCode,
            entity.Author,
            entity.Initials,
            entity.PostedAt is null ? null : DateTime.SpecifyKind(entity.PostedAt.Value, DateTimeKind.Utc),
            entity.Text);
    }
}
