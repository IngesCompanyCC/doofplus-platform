using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Implements the deviation command service of the Quality &amp; Compliance bounded context.
/// </summary>
public class DeviationCommandService(
    IDeviationRepository deviationRepository,
    IUnitOfWork unitOfWork) : IDeviationCommandService
{
    /// <inheritdoc />
    public async Task<Result<Deviation>> Handle(RegisterDeviationCommand command, CancellationToken cancellationToken)
    {
        if (await deviationRepository.ExistsByCodeAsync(command.Code, cancellationToken))
            return Result<Deviation>.Failure(QualityErrors.DuplicateDeviationCode(command.Code),
                QualityError.DuplicateDeviationCode);

        var result = QualityDomainRules.Create(() => new Deviation(command));
        if (result.IsFailure) return result;

        await deviationRepository.AddAsync(result.Value!, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public Task<Result<Deviation>> Handle(ClassifyDeviationCommand command, CancellationToken cancellationToken)
    {
        return Update(command.DeviationId, deviation => deviation.Classify(command.Severity), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Deviation>> Handle(StartDeviationInvestigationCommand command,
        CancellationToken cancellationToken)
    {
        return Update(command.DeviationId, deviation => deviation.StartInvestigation(), cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Deviation>> Handle(RegisterRootCauseCommand command, CancellationToken cancellationToken)
    {
        return Update(command.DeviationId, deviation => deviation.RegisterRootCause(command.RootCause),
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Deviation>> Handle(AddCapaActionCommand command, CancellationToken cancellationToken)
    {
        return Update(command.DeviationId,
            deviation => deviation.AddCapaAction(new CapaAction(command.Type, command.Description,
                command.ResponsibleId, command.DueDate)),
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Deviation>> Handle(UpdateCapaActionProgressCommand command,
        CancellationToken cancellationToken)
    {
        return Update(command.DeviationId,
            deviation => deviation.UpdateCapaActionProgress(command.CapaActionId, command.Progress),
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Deviation>> Handle(VerifyCapaActionCommand command, CancellationToken cancellationToken)
    {
        return Update(command.DeviationId, deviation => deviation.VerifyCapaAction(command.CapaActionId),
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<Result<Deviation>> Handle(CloseDeviationCommand command, CancellationToken cancellationToken)
    {
        return Update(command.DeviationId, deviation => deviation.Close(), cancellationToken);
    }

    private async Task<Result<Deviation>> Update(DeviationId id, Action<Deviation> behavior,
        CancellationToken cancellationToken)
    {
        var deviation = await deviationRepository.FindByIdAsync(id, cancellationToken);
        if (deviation is null)
            return Result<Deviation>.Failure(QualityErrors.DeviationNotFound(id), QualityError.DeviationNotFound);

        var result = QualityDomainRules.Apply(deviation, behavior);
        if (result.IsFailure) return result;

        await unitOfWork.CompleteAsync(cancellationToken);
        return result;
    }
}
