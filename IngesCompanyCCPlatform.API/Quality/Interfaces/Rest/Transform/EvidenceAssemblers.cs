using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>Evidence</c> from the resources. Enums are accepted in any case and with hyphens.
/// </summary>
public static class EvidenceCommandFromResourceAssembler
{
    /// <summary>
    ///     Builds the command to create a evidence.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateEvidenceCommand ToCommandFromResource(CreateEvidenceResource resource)
    {
        return new CreateEvidenceCommand(
            resource.RecordCode ?? string.Empty,
            resource.FileName ?? string.Empty,
            resource.Detail ?? string.Empty,
            resource.Status is null ? EEvidenceStatus.Attached : EnumParser.Parse<EEvidenceStatus>(resource.Status, "quality.evidence.error.status.invalid"));
    }

    /// <summary>
    ///     Builds the command to replace a evidence.
    /// </summary>
    /// <param name="evidenceId">The id of the evidence.</param>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static UpdateEvidenceCommand ToCommandFromResource(int evidenceId, UpdateEvidenceResource resource)
    {
        return new UpdateEvidenceCommand(
            evidenceId,
            resource.RecordCode ?? string.Empty,
            resource.FileName ?? string.Empty,
            resource.Detail ?? string.Empty,
            resource.Status is null ? EEvidenceStatus.Attached : EnumParser.Parse<EEvidenceStatus>(resource.Status, "quality.evidence.error.status.invalid"));
    }
}

/// <summary>
///     Builds the resource of a evidence.
/// </summary>
public static class EvidenceResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The evidence.</param>
    /// <returns>The resource.</returns>
    public static EvidenceResource ToResourceFromEntity(Evidence entity)
    {
        return new EvidenceResource(
            entity.Id.Value,
            entity.RecordCode,
            entity.FileName,
            entity.Detail,
            entity.Status.ToString());
    }
}
