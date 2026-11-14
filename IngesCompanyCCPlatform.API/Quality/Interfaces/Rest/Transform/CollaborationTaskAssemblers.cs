using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>CollaborationTask</c> from the resources.
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
            resource.Title,
            resource.Description ?? string.Empty,
            resource.AssignedTo,
            resource.CreatedBy,
            resource.RelatedDeviationId,
            resource.DueDate);
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
            entity.Description,
            entity.AssignedTo,
            entity.CreatedBy,
            entity.RelatedDeviationId,
            entity.DueDate,
            entity.Status.ToString(),
            entity.CompletedAt is null ? null : DateTime.SpecifyKind(entity.CompletedAt.Value, DateTimeKind.Utc));
    }
}
