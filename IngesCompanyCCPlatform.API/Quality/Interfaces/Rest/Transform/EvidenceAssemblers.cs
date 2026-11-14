using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>Evidence</c> from the resources.
/// </summary>
public static class EvidenceCommandFromResourceAssembler
{
    private const string TypeInvalidMessageKey = "quality.evidence.error.type.invalid";

    /// <summary>
    ///     Builds the command to create a evidence.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateEvidenceCommand ToCommandFromResource(CreateEvidenceResource resource)
    {
        return new CreateEvidenceCommand(
            resource.Title,
            resource.Description ?? string.Empty,
            EnumParser.Parse<EEvidenceType>(resource.Type, TypeInvalidMessageKey),
            resource.FileReference,
            resource.DeviationId,
            resource.DocumentId,
            resource.UploadedBy);
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
            entity.Title,
            entity.Description,
            entity.Type.ToString(),
            entity.FileReference,
            entity.DeviationId,
            entity.DocumentId,
            entity.UploadedBy,
            entity.Status.ToString(),
            DateTime.SpecifyKind(entity.UploadedAt, DateTimeKind.Utc),
            entity.VerifiedAt is null ? null : DateTime.SpecifyKind(entity.VerifiedAt.Value, DateTimeKind.Utc));
    }
}
