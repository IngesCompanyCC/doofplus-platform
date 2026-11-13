using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.CommandServices;

/// <summary>
///     Command service of the alerts.
/// </summary>
public interface IAlertCommandService
{
    /// <summary>
    ///     Raises an alert by hand; fails if the code already exists.
    /// </summary>
    Task<Result<Alert>> Handle(CreateAlertCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Records the handling of an alert; fails if it does not exist.
    /// </summary>
    Task<Result<Alert>> Handle(UpdateAlertCommand command, CancellationToken cancellationToken);
}
