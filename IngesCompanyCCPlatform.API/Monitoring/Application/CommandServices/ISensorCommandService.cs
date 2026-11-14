using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Monitoring.Application.CommandServices;

/// <summary>
///     Command service of the sensors.
/// </summary>
public interface ISensorCommandService
{
    /// <summary>
    ///     Registers a sensor on an existing piece of equipment; fails if the code or the device is taken.
    /// </summary>
    Task<Result<Sensor>> Handle(CreateSensorCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Updates a sensor; fails if it or its equipment does not exist.
    /// </summary>
    Task<Result<Sensor>> Handle(UpdateSensorCommand command, CancellationToken cancellationToken);
}
