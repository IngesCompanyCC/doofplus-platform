using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>CapaAction</c> from the resources. Enums are accepted in any case and with hyphens.
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
            resource.CapaCode ?? string.Empty,
            resource.Code ?? string.Empty,
            resource.Type is null ? null : EnumParser.Parse<ECapaType>(resource.Type, "quality.capaAction.error.type.invalid"),
            resource.Title ?? string.Empty,
            resource.Owner ?? string.Empty,
            resource.DueAt,
            resource.Status is null ? EActionStatus.Open : EnumParser.Parse<EActionStatus>(resource.Status, "quality.capaAction.error.status.invalid"),
            resource.Evidence ?? string.Empty,
            resource.EvidenceAttached ?? false);
    }

    /// <summary>
    ///     Builds the command to replace a capa action.
    /// </summary>
    /// <param name="capaActionId">The id of the capa action.</param>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static UpdateCapaActionCommand ToCommandFromResource(int capaActionId, UpdateCapaActionResource resource)
    {
        return new UpdateCapaActionCommand(
            capaActionId,
            resource.CapaCode ?? string.Empty,
            resource.Code ?? string.Empty,
            resource.Type is null ? null : EnumParser.Parse<ECapaType>(resource.Type, "quality.capaAction.error.type.invalid"),
            resource.Title ?? string.Empty,
            resource.Owner ?? string.Empty,
            resource.DueAt,
            resource.Status is null ? EActionStatus.Open : EnumParser.Parse<EActionStatus>(resource.Status, "quality.capaAction.error.status.invalid"),
            resource.Evidence ?? string.Empty,
            resource.EvidenceAttached ?? false);
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
            entity.CapaCode,
            entity.Code,
            entity.Type.ToString(),
            entity.Title,
            entity.Owner,
            entity.DueAt,
            entity.Status.ToString(),
            entity.Evidence,
            entity.EvidenceAttached);
    }
}
