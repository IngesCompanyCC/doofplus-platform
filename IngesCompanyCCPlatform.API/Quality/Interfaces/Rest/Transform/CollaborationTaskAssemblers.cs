using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>CollaborationTask</c> from the resources. Enums are accepted in any case and with hyphens.
/// </summary>
public static class CollaborationTaskCommandFromResourceAssembler
{
    /// <summary>
    ///     Builds the command to create a collaboration task.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateCollaborationTaskCommand ToCommandFromResource(CreateCollaborationTaskResource resource)
    {
        return new CreateCollaborationTaskCommand(
            resource.Title ?? string.Empty,
            resource.RecordCode ?? string.Empty,
            resource.Detail ?? string.Empty,
            resource.Owner ?? string.Empty,
            resource.DueAt,
            resource.Status is null ? ETaskStatus.ToDo : EnumParser.Parse<ETaskStatus>(resource.Status, "quality.collaborationTask.error.status.invalid"));
    }
}

/// <summary>
///     Builds the resource of a collaboration task.
/// </summary>
public static class CollaborationTaskResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The collaboration task.</param>
    /// <returns>The resource.</returns>
    public static CollaborationTaskResource ToResourceFromEntity(CollaborationTask entity)
    {
        return new CollaborationTaskResource(
            entity.Id.Value,
            entity.Title,
            entity.RecordCode,
            entity.Detail,
            entity.Owner,
            entity.DueAt is null ? null : DateTime.SpecifyKind(entity.DueAt.Value, DateTimeKind.Utc),
            entity.Status.ToString());
    }
}
