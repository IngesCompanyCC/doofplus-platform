using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Converts a <see cref="QualityDocument" /> aggregate into a <see cref="QualityDocumentResource" />.
/// </summary>
public static class QualityDocumentResourceFromEntityAssembler
{
    public static QualityDocumentResource ToResourceFromEntity(QualityDocument entity)
    {
        return new QualityDocumentResource(
            entity.Id,
            entity.Code,
            entity.Title,
            entity.Type.ToString(),
            entity.Status.ToString(),
            entity.CurrentVersion.VersionNumber,
            entity.Versions.OrderBy(version => version.Id).Select(ToResourceFromEntity));
    }

    private static DocumentVersionResource ToResourceFromEntity(DocumentVersion entity)
    {
        return new DocumentVersionResource(entity.Id, entity.VersionNumber, entity.FileUrl, entity.ChangeReason,
            entity.AuthorId, entity.ApprovalSignatureId?.Value);
    }
}

/// <summary>
///     Converts the request resources of the quality documents into commands.
/// </summary>
public static class QualityDocumentCommandFromResourceAssembler
{
    public static CreateQualityDocumentCommand ToCommandFromResource(CreateQualityDocumentResource resource)
    {
        return new CreateQualityDocumentCommand(resource.Code, resource.Title,
            QualityEnumParser.Parse<EDocumentType>(resource.Type, "document type"), resource.FileUrl,
            new UserId(resource.AuthorId));
    }

    public static ApproveQualityDocumentCommand ToCommandFromResource(int qualityDocumentId,
        ApproveQualityDocumentResource resource)
    {
        return new ApproveQualityDocumentCommand(qualityDocumentId, new UserId(resource.ApproverId),
            new ElectronicSignatureId(resource.SignatureId));
    }

    public static RejectQualityDocumentCommand ToCommandFromResource(int qualityDocumentId,
        RejectQualityDocumentResource resource)
    {
        return new RejectQualityDocumentCommand(qualityDocumentId, resource.Reason);
    }

    public static PublishDocumentVersionCommand ToCommandFromResource(int qualityDocumentId,
        PublishDocumentVersionResource resource)
    {
        return new PublishDocumentVersionCommand(qualityDocumentId, resource.FileUrl, resource.ChangeReason,
            new UserId(resource.AuthorId));
    }
}
