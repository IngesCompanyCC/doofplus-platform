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
///     Command service of <see cref="Deviation" />.
/// </summary>
public class DeviationCommandService(
    IDeviationRepository deviationRepository,
    IUnitOfWork unitOfWork) : IDeviationCommandService
{
    /// <inheritdoc />
    public async Task<Result<Deviation>> Handle(CreateDeviationCommand command, CancellationToken cancellationToken)
    {
        if (await deviationRepository.ExistsByCodeAsync(command.Code.Trim(), cancellationToken))
            return Result<Deviation>.Failure(DeviationErrors.DuplicateCode, QualityError.DuplicateDeviationCode);

        var deviation = new Deviation(command);
        await deviationRepository.AddAsync(deviation, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Deviation>.Success(deviation);
    }

    /// <inheritdoc />
    public async Task<Result<Deviation>> Handle(UpdateDeviationCommand command, CancellationToken cancellationToken)
    {
        var deviation = await deviationRepository.FindByIdAsync(command.DeviationId, cancellationToken);
        if (deviation is null)
            return Result<Deviation>.Failure(DeviationErrors.NotFoundById(command.DeviationId), QualityError.DeviationNotFound);

        deviation.Update(command);
        deviationRepository.Update(deviation);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Deviation>.Success(deviation);
    }
}
