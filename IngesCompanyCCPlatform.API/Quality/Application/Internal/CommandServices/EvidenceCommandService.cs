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
}
