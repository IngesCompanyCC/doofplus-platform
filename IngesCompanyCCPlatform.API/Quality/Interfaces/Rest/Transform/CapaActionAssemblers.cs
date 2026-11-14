using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>CapaAction</c> from the resources.
/// </summary>
public static class CapaActionCommandFromResourceAssembler
{


    /// <summary>
    ///     Builds the command to create a capa action.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateCapaActionCommand ToCommandFromResource(CreateCapaActionResource resource)
    {
        return new CreateCapaActionCommand(
            resource.CapaPlanId,
            resource.Title,
            resource.Description,
            resource.AssignedTo,
            resource.DueDate);
    }
}

/// <summary>
///     Builds the resource of a capa action.
/// </summary>
public static class CapaActionResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The capa action.</param>
    /// <returns>The resource.</returns>
    public static CapaActionResource ToResourceFromEntity(CapaAction entity)
    {
        return new CapaActionResource(
            entity.Id.Value,
            entity.CapaPlanId,
            entity.Title,
            entity.Description,
            entity.AssignedTo,
            entity.DueDate,
            entity.Status.ToString(),
            entity.CompletedAt is null ? null : DateTime.SpecifyKind(entity.CompletedAt.Value, DateTimeKind.Utc));
    }
}
