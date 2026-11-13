using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.CommandServices;

/// <summary>
///     Command service of the telemetry readings.
/// </summary>
public interface IReadingCommandService
{
    /// <summary>
    ///     Records a reading of an existing sensor and raises an alert if it is out of range.
    /// </summary>
    Task<Result<Reading>> Handle(CreateReadingCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Records a reading sent by ThingsBoard for the sensor of its device (TS04).
    /// </summary>
    Task<Result<Reading>> Handle(RecordTelemetryCommand command, CancellationToken cancellationToken);
}
