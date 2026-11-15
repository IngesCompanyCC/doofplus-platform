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
///     Command service of <see cref="CapaPlan" />.
/// </summary>
public class CapaPlanCommandService(
    ICapaPlanRepository capaPlanRepository,
    IUnitOfWork unitOfWork) : ICapaPlanCommandService
{
    /// <inheritdoc />
    public async Task<Result<CapaPlan>> Handle(CreateCapaPlanCommand command, CancellationToken cancellationToken)
    {
        if (await capaPlanRepository.ExistsByCodeAsync(command.Code.Trim(), cancellationToken))
            return Result<CapaPlan>.Failure(CapaPlanErrors.DuplicateCode, QualityError.DuplicateCapaPlanCode);

        var capaPlan = new CapaPlan(command);
        await capaPlanRepository.AddAsync(capaPlan, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<CapaPlan>.Success(capaPlan);
    }

    /// <inheritdoc />
    public async Task<Result<CapaPlan>> Handle(UpdateCapaPlanCommand command, CancellationToken cancellationToken)
    {
        var capaPlan = await capaPlanRepository.FindByIdAsync(command.CapaPlanId, cancellationToken);
        if (capaPlan is null)
            return Result<CapaPlan>.Failure(CapaPlanErrors.NotFoundById(command.CapaPlanId), QualityError.CapaPlanNotFound);

        capaPlan.Update(command);
        capaPlanRepository.Update(capaPlan);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<CapaPlan>.Success(capaPlan);
    }
}
