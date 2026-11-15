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
///     Command service of <see cref="Evidence" />.
/// </summary>
public class EvidenceCommandService(
    IEvidenceRepository evidenceRepository,
    IUnitOfWork unitOfWork) : IEvidenceCommandService
{
    /// <inheritdoc />
    public async Task<Result<Evidence>> Handle(CreateEvidenceCommand command, CancellationToken cancellationToken)
    {
        var evidence = new Evidence(command);
        await evidenceRepository.AddAsync(evidence, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Evidence>.Success(evidence);
    }

    /// <inheritdoc />
    public async Task<Result<Evidence>> Handle(UpdateEvidenceCommand command, CancellationToken cancellationToken)
    {
        var evidence = await evidenceRepository.FindByIdAsync(command.EvidenceId, cancellationToken);
        if (evidence is null)
            return Result<Evidence>.Failure(EvidenceErrors.NotFoundById(command.EvidenceId), QualityError.EvidenceNotFound);

        evidence.Update(command);
        evidenceRepository.Update(evidence);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Evidence>.Success(evidence);
    }

    /// <inheritdoc />
    public async Task<Result<Evidence>> Handle(DeleteEvidenceCommand command, CancellationToken cancellationToken)
    {
        var evidence = await evidenceRepository.FindByIdAsync(command.EvidenceId, cancellationToken);
        if (evidence is null)
            return Result<Evidence>.Failure(EvidenceErrors.NotFoundById(command.EvidenceId), QualityError.EvidenceNotFound);

        if (!evidence.CanBeRemoved())
            return Result<Evidence>.Failure(EvidenceErrors.Reviewed, QualityError.EvidenceReviewed);

        evidenceRepository.Remove(evidence);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Evidence>.Success(evidence);
    }
}
