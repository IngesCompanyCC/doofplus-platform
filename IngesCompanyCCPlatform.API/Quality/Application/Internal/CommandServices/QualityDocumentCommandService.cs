using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Implements the quality document command service of the Quality &amp; Compliance bounded context.
/// </summary>
public class QualityDocumentCommandService(
    IQualityDocumentRepository qualityDocumentRepository,
    IUnitOfWork unitOfWork) : IQualityDocumentCommandService
{
    /// <inheritdoc />
    public async Task<Result<QualityDocument>> Handle(CreateQualityDocumentCommand command,
        CancellationToken cancellationToken)
    {
        if (await qualityDocumentRepository.ExistsByCodeAsync(command.Code, cancellationToken))
            return Result<QualityDocument>.Failure(QualityErrors.DuplicateQualityDocumentCode(command.Code),
                QualityError.DuplicateQualityDocumentCode);

        var result = QualityDomainRules.Create(() => new QualityDocument(command));
        if (result.IsFailure) return result;

        await qualityDocumentRepository.AddAsync(result.Value!, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public Task<Result<QualityDocument>> Handle(SubmitQualityDocumentForReviewCommand command,
        CancellationToken cancellationToken)
    {
        return Update(command.QualityDocumentId, document => document.SubmitForReview(), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<QualityDocument>> Handle(ApproveQualityDocumentCommand command,
        CancellationToken cancellationToken)
    {
        return Update(command.QualityDocumentId,
            document => document.Approve(command.ApproverId, command.SignatureId), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<QualityDocument>> Handle(RejectQualityDocumentCommand command,
        CancellationToken cancellationToken)
    {
        return Update(command.QualityDocumentId, document => document.Reject(command.Reason), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<QualityDocument>> Handle(PublishDocumentVersionCommand command,
        CancellationToken cancellationToken)
    {
        return Update(command.QualityDocumentId,
            document => document.PublishNewVersion(command.FileUrl, command.ChangeReason, command.AuthorId),
            cancellationToken);
    }

    private async Task<Result<QualityDocument>> Update(QualityDocumentId id, Action<QualityDocument> behavior,
        CancellationToken cancellationToken)
    {
        var document = await qualityDocumentRepository.FindByIdAsync(id, cancellationToken);
        if (document is null)
            return Result<QualityDocument>.Failure(QualityErrors.QualityDocumentNotFound(id),
                QualityError.QualityDocumentNotFound);

        var result = QualityDomainRules.Apply(document, behavior);
        if (result.IsFailure) return result;

        await unitOfWork.CompleteAsync(cancellationToken);
        return result;
    }
}
