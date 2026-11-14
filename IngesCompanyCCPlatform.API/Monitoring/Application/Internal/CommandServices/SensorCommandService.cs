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
///     Command service of the sensors.
/// </summary>
public class SensorCommandService(
    ISensorRepository sensorRepository,
    IEquipmentRepository equipmentRepository,
    IUnitOfWork unitOfWork) : ISensorCommandService
{
    /// <inheritdoc />
    public async Task<Result<Sensor>> Handle(CreateSensorCommand command, CancellationToken cancellationToken)
    {
        if (await sensorRepository.ExistsByCodeAsync(command.Code, cancellationToken))
            return Result<Sensor>.Failure(SensorErrors.DuplicateCode, MonitoringError.DuplicateSensorCode);
        if (await sensorRepository.ExistsByDeviceIdAsync(command.DeviceId, cancellationToken))
            return Result<Sensor>.Failure(SensorErrors.DuplicateDeviceId, MonitoringError.DuplicateDeviceId);
        if (!await equipmentRepository.ExistsByCodeAsync(command.EquipmentCode, cancellationToken))
            return Result<Sensor>.Failure(EquipmentErrors.NotFoundByCode(command.EquipmentCode.Value),
                MonitoringError.EquipmentNotFound);

        var sensor = new Sensor(command);
        await sensorRepository.AddAsync(sensor, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Sensor>.Success(sensor);
    }

    /// <inheritdoc />
    public async Task<Result<Sensor>> Handle(UpdateSensorCommand command, CancellationToken cancellationToken)
    {
        var sensor = await sensorRepository.FindByIdAsync(command.SensorId, cancellationToken);
        if (sensor is null)
            return Result<Sensor>.Failure(SensorErrors.NotFoundById(command.SensorId), MonitoringError.SensorNotFound);
        if (!await equipmentRepository.ExistsByCodeAsync(command.EquipmentCode, cancellationToken))
            return Result<Sensor>.Failure(EquipmentErrors.NotFoundByCode(command.EquipmentCode.Value),
                MonitoringError.EquipmentNotFound);

        sensor.Update(command);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Sensor>.Success(sensor);
    }
}
