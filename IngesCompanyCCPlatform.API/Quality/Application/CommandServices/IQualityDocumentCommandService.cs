using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Represents the quality document command service of the Quality &amp; Compliance bounded context.
/// </summary>
public interface IQualityDocumentCommandService
{
    /// <summary>Creates a quality document with its first version.</summary>
    Task<Result<QualityDocument>> Handle(CreateQualityDocumentCommand command, CancellationToken cancellationToken);

    /// <summary>Sends the current version of a document to review.</summary>
    Task<Result<QualityDocument>> Handle(SubmitQualityDocumentForReviewCommand command, CancellationToken cancellationToken);

    /// <summary>Approves the current version of a document.</summary>
    Task<Result<QualityDocument>> Handle(ApproveQualityDocumentCommand command, CancellationToken cancellationToken);

    /// <summary>Sends a document in review back to draft.</summary>
    Task<Result<QualityDocument>> Handle(RejectQualityDocumentCommand command, CancellationToken cancellationToken);

    /// <summary>Adds a new revision to an approved document.</summary>
    Task<Result<QualityDocument>> Handle(PublishDocumentVersionCommand command, CancellationToken cancellationToken);
}
