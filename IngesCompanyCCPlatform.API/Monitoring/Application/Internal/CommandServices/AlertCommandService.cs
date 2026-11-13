using IngesCompanyCCPlatform.API.Monitoring.Application.CommandServices;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.Internal.CommandServices;

/// <summary>
///     Command service of the alerts.
/// </summary>
public class AlertCommandService(IAlertRepository alertRepository, IUnitOfWork unitOfWork) : IAlertCommandService
{
    /// <inheritdoc />
    public async Task<Result<Alert>> Handle(CreateAlertCommand command, CancellationToken cancellationToken)
    {
        if (await alertRepository.ExistsByCodeAsync(command.Code, cancellationToken))
            return Result<Alert>.Failure(AlertErrors.DuplicateCode, MonitoringError.DuplicateAlertCode);

        var alert = new Alert(command);
        await alertRepository.AddAsync(alert, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Alert>.Success(alert);
    }

    /// <inheritdoc />
    public async Task<Result<Alert>> Handle(UpdateAlertCommand command, CancellationToken cancellationToken)
    {
        var alert = await alertRepository.FindByIdAsync(command.AlertId, cancellationToken);
        if (alert is null)
            return Result<Alert>.Failure(AlertErrors.NotFoundById(command.AlertId), MonitoringError.AlertNotFound);

        alert.Update(command);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Alert>.Success(alert);
    }
}
