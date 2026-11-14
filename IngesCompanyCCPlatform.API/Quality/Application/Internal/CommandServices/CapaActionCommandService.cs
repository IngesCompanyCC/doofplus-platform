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
///     Command service of <see cref="CapaAction" />.
/// </summary>
public class CapaActionCommandService(
    ICapaActionRepository capaActionRepository,
    ICapaPlanRepository capaPlanRepository,
    IUnitOfWork unitOfWork) : ICapaActionCommandService
{
    /// <inheritdoc />
    public async Task<Result<CapaAction>> Handle(CreateCapaActionCommand command, CancellationToken cancellationToken)
    {
        if (await capaPlanRepository.FindByIdAsNoTrackingAsync(command.CapaPlanId, cancellationToken) is null)
            return Result<CapaAction>.Failure(CapaPlanErrors.NotFoundById(command.CapaPlanId), QualityError.CapaPlanNotFound);

        var capaAction = new CapaAction(command);
        await capaActionRepository.AddAsync(capaAction, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<CapaAction>.Success(capaAction);
    }
}
