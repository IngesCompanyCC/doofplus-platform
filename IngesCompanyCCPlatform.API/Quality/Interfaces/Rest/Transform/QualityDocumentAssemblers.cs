using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>QualityDocument</c> from the resources. Enums are accepted in any case and with hyphens.
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
            resource.Code ?? string.Empty,
            resource.Version ?? string.Empty,
            resource.Title ?? string.Empty,
            resource.Status is null ? EDocumentStatus.Draft : EnumParser.Parse<EDocumentStatus>(resource.Status, "quality.qualityDocument.error.status.invalid"),
            resource.Author ?? string.Empty,
            resource.TechnicalReviewer ?? string.Empty,
            resource.Approver ?? string.Empty,
            resource.ChangedAt,
            resource.Purpose ?? string.Empty,
            resource.Scope ?? string.Empty,
            resource.Procedure ?? string.Empty,
            resource.RevisionSummary ?? string.Empty,
            resource.ReviewCycleMonths ?? 12);
    }

    /// <summary>
    ///     Builds the command to replace a quality document.
    /// </summary>
    /// <param name="qualityDocumentId">The id of the quality document.</param>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static UpdateQualityDocumentCommand ToCommandFromResource(int qualityDocumentId, UpdateQualityDocumentResource resource)
    {
        return new UpdateQualityDocumentCommand(
            qualityDocumentId,
            resource.Code ?? string.Empty,
            resource.Version ?? string.Empty,
            resource.Title ?? string.Empty,
            resource.Status is null ? EDocumentStatus.Draft : EnumParser.Parse<EDocumentStatus>(resource.Status, "quality.qualityDocument.error.status.invalid"),
            resource.Author ?? string.Empty,
            resource.TechnicalReviewer ?? string.Empty,
            resource.Approver ?? string.Empty,
            resource.ChangedAt,
            resource.Purpose ?? string.Empty,
            resource.Scope ?? string.Empty,
            resource.Procedure ?? string.Empty,
            resource.RevisionSummary ?? string.Empty,
            resource.ReviewCycleMonths ?? 12);
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
            entity.Version,
            entity.Title,
            entity.Status.ToString(),
            entity.Author,
            entity.TechnicalReviewer,
            entity.Approver,
            entity.ChangedAt,
            entity.Purpose,
            entity.Scope,
            entity.Procedure,
            entity.RevisionSummary,
            entity.ReviewCycleMonths);
    }
}
