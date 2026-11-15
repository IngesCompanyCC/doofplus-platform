using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Command service of <see cref="QualityDocument" />.
/// </summary>
public class QualityDocumentCommandService(
    IQualityDocumentRepository qualityDocumentRepository,
    IUnitOfWork unitOfWork) : IQualityDocumentCommandService
{
    /// <inheritdoc />
    public async Task<Result<QualityDocument>> Handle(CreateQualityDocumentCommand command, CancellationToken cancellationToken)
    {
        var qualityDocument = new QualityDocument(command);
        await qualityDocumentRepository.AddAsync(qualityDocument, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<QualityDocument>.Success(qualityDocument);
    }

    /// <inheritdoc />
    public async Task<Result<QualityDocument>> Handle(UpdateQualityDocumentCommand command, CancellationToken cancellationToken)
    {
        var qualityDocument = await qualityDocumentRepository.FindByIdAsync(command.QualityDocumentId, cancellationToken);
        if (qualityDocument is null)
            return Result<QualityDocument>.Failure(QualityDocumentErrors.NotFoundById(command.QualityDocumentId), QualityError.QualityDocumentNotFound);

        qualityDocument.Update(command);
        qualityDocumentRepository.Update(qualityDocument);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<QualityDocument>.Success(qualityDocument);
    }
}
