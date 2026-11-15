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
    IUnitOfWork unitOfWork) : ICapaActionCommandService
{
    /// <inheritdoc />
    public async Task<Result<CapaAction>> Handle(CreateCapaActionCommand command, CancellationToken cancellationToken)
    {
        var capaAction = new CapaAction(command);
        await capaActionRepository.AddAsync(capaAction, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<CapaAction>.Success(capaAction);
    }

    /// <inheritdoc />
    public async Task<Result<CapaAction>> Handle(UpdateCapaActionCommand command, CancellationToken cancellationToken)
    {
        var capaAction = await capaActionRepository.FindByIdAsync(command.CapaActionId, cancellationToken);
        if (capaAction is null)
            return Result<CapaAction>.Failure(CapaActionErrors.NotFoundById(command.CapaActionId), QualityError.CapaActionNotFound);

        capaAction.Update(command);
        capaActionRepository.Update(capaAction);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<CapaAction>.Success(capaAction);
    }
}
