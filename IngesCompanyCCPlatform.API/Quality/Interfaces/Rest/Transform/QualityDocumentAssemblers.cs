using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>QualityDocument</c> from the resources.
/// </summary>
public static class QualityDocumentCommandFromResourceAssembler
{


    /// <summary>
    ///     Builds the command to create a quality document.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateQualityDocumentCommand ToCommandFromResource(CreateQualityDocumentResource resource)
    {
        return new CreateQualityDocumentCommand(
            resource.Code,
            resource.Title,
            resource.Description ?? string.Empty,
            resource.DocumentType ?? string.Empty,
            resource.FileReference ?? string.Empty,
            resource.Version ?? string.Empty,
            resource.OwnerId);
    }
}

/// <summary>
///     Builds the resource of a quality document.
/// </summary>
public static class QualityDocumentResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The quality document.</param>
    /// <returns>The resource.</returns>
    public static QualityDocumentResource ToResourceFromEntity(QualityDocument entity)
    {
        return new QualityDocumentResource(
            entity.Id.Value,
            entity.Code,
            entity.Title,
            entity.Description,
            entity.DocumentType,
            entity.FileReference,
            entity.Version,
            entity.OwnerId,
            entity.Status.ToString(),
            entity.ApprovedAt is null ? null : DateTime.SpecifyKind(entity.ApprovedAt.Value, DateTimeKind.Utc));
    }
}
