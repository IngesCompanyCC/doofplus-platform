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
///     Command service of the telemetry readings.
/// </summary>
/// <remarks>
///     Every reading is checked against the limits of its sensor. A reading outside them raises an alert in the
///     same unit of work, so both are saved together.
/// </remarks>
public class ReadingCommandService(
    IReadingRepository readingRepository,
    ISensorRepository sensorRepository,
    IAlertRepository alertRepository,
    IUnitOfWork unitOfWork) : IReadingCommandService
{
    /// <inheritdoc />
    public async Task<Result<Reading>> Handle(CreateReadingCommand command, CancellationToken cancellationToken)
    {
        var sensor = await sensorRepository.FindByCodeAsNoTrackingAsync(command.SensorCode, cancellationToken);
        if (sensor is null)
            return Result<Reading>.Failure(SensorErrors.NotFoundByCode(command.SensorCode.Value),
                MonitoringError.SensorNotFound);

        return await RecordAsync(sensor, command, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Result<Reading>> Handle(RecordTelemetryCommand command, CancellationToken cancellationToken)
    {
        var sensor = await sensorRepository.FindByDeviceIdAsNoTrackingAsync(command.DeviceId, cancellationToken);
        if (sensor is null)
            return Result<Reading>.Failure(SensorErrors.NotFoundByDeviceId(command.DeviceId.Value),
                MonitoringError.SensorNotFound);

        var createReadingCommand = new CreateReadingCommand(sensor.Code, sensor.AssignedBatch, command.Value,
            command.RecordedAt, Reading.DefaultSource);
        return await RecordAsync(sensor, createReadingCommand, cancellationToken);
    }

    private async Task<Result<Reading>> RecordAsync(Sensor sensor, CreateReadingCommand command,
        CancellationToken cancellationToken)
    {
        // Without a batch, the reading belongs to the batch the sensor is monitoring
        if (string.IsNullOrWhiteSpace(command.BatchCode))
            command = command with { BatchCode = sensor.AssignedBatch };

        var reading = new Reading(command);
        await readingRepository.AddAsync(reading, cancellationToken);

        if (sensor.IsOutOfRange(reading.Value))
        {
            var alert = Alert.FromOutOfRangeReading(sensor, reading);
            if (!await alertRepository.ExistsByCodeAsync(alert.Code, cancellationToken))
                await alertRepository.AddAsync(alert, cancellationToken);
        }

        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Reading>.Success(reading);
    }
}
