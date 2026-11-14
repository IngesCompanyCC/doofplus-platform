using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>CapaPlan</c> from the resources.
/// </summary>
public static class CapaPlanCommandFromResourceAssembler
{


    /// <summary>
    ///     Builds the command to create a capa plan.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateCapaPlanCommand ToCommandFromResource(CreateCapaPlanResource resource)
    {
        return new CreateCapaPlanCommand(
            resource.DeviationId,
            resource.Title,
            resource.Description,
            resource.DueDate,
            resource.Responsible);
    }
}

/// <summary>
///     Builds the resource of a capa plan.
/// </summary>
public static class CapaPlanResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The capa plan.</param>
    /// <returns>The resource.</returns>
    public static CapaPlanResource ToResourceFromEntity(CapaPlan entity)
    {
        return new CapaPlanResource(
            entity.Id.Value,
            entity.DeviationId,
            entity.Title,
            entity.Description,
            entity.DueDate,
            entity.Responsible,
            entity.Status.ToString(),
            entity.CompletedAt is null ? null : DateTime.SpecifyKind(entity.CompletedAt.Value, DateTimeKind.Utc),
            entity.CreatedAt);
    }
}
