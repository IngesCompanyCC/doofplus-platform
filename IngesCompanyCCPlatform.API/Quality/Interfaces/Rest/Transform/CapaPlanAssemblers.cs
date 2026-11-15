using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>CapaPlan</c> from the resources. Enums are accepted in any case and with hyphens.
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
            resource.Code ?? string.Empty,
            resource.Title ?? string.Empty,
            resource.SourceDeviation ?? string.Empty,
            resource.Owner ?? string.Empty,
            resource.Reviewer ?? string.Empty,
            resource.RootCause ?? string.Empty,
            resource.AcceptanceCriterion ?? string.Empty,
            resource.EffectivenessCheckAt,
            resource.Status is null ? ECapaStatus.Draft : EnumParser.Parse<ECapaStatus>(resource.Status, "quality.capaPlan.error.status.invalid"));
    }

    /// <summary>
    ///     Builds the command to replace a capa plan.
    /// </summary>
    /// <param name="capaPlanId">The id of the capa plan.</param>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static UpdateCapaPlanCommand ToCommandFromResource(int capaPlanId, UpdateCapaPlanResource resource)
    {
        return new UpdateCapaPlanCommand(
            capaPlanId,
            resource.Code ?? string.Empty,
            resource.Title ?? string.Empty,
            resource.SourceDeviation ?? string.Empty,
            resource.Owner ?? string.Empty,
            resource.Reviewer ?? string.Empty,
            resource.RootCause ?? string.Empty,
            resource.AcceptanceCriterion ?? string.Empty,
            resource.EffectivenessCheckAt,
            resource.Status is null ? ECapaStatus.Draft : EnumParser.Parse<ECapaStatus>(resource.Status, "quality.capaPlan.error.status.invalid"));
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
            entity.Code,
            entity.Title,
            entity.SourceDeviation,
            entity.Owner,
            entity.Reviewer,
            entity.RootCause,
            entity.AcceptanceCriterion,
            entity.EffectivenessCheckAt,
            entity.Status.ToString());
    }
}
