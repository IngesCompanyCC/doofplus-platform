using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of the sensors from the resources.
/// </summary>
public static class SensorCommandFromResourceAssembler
{
    private const string TypeInvalidMessageKey = "monitoring.sensor.error.type.invalid";
    private const string StatusInvalidMessageKey = "monitoring.sensor.error.status.invalid";

    /// <summary>
    ///     Builds the command to register a sensor.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when a code, the type or the status is not valid.</exception>
    public static CreateSensorCommand ToCommandFromResource(CreateSensorResource resource)
    {
        return new CreateSensorCommand(
            new SensorCode(resource.Code),
            new DeviceId(resource.DeviceId),
            EnumParser.Parse<ESensorType>(resource.Type, TypeInvalidMessageKey),
            resource.Unit ?? string.Empty,
            new EquipmentCode(resource.EquipmentCode),
            resource.LowerLimit,
            resource.UpperLimit,
            EnumParser.ParseOrDefault(resource.Status, ESensorStatus.Active, StatusInvalidMessageKey),
            resource.AssignedBatch ?? string.Empty);
    }

    /// <summary>
    ///     Builds the command to update a sensor.
    /// </summary>
    /// <param name="sensorId">The id from the route.</param>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when the equipment code, the type or the status is not valid.</exception>
    public static UpdateSensorCommand ToCommandFromResource(int sensorId, UpdateSensorResource resource)
    {
        return new UpdateSensorCommand(
            sensorId,
            EnumParser.Parse<ESensorType>(resource.Type, TypeInvalidMessageKey),
            resource.Unit ?? string.Empty,
            new EquipmentCode(resource.EquipmentCode),
            resource.LowerLimit,
            resource.UpperLimit,
            EnumParser.ParseOrDefault(resource.Status, ESensorStatus.Active, StatusInvalidMessageKey),
            resource.AssignedBatch ?? string.Empty);
    }
}

/// <summary>
///     Builds the resource of a sensor.
/// </summary>
public static class SensorResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The sensor.</param>
    /// <returns>The resource.</returns>
    public static SensorResource ToResourceFromEntity(Sensor entity)
    {
        return new SensorResource(entity.Id.Value, entity.Code.Value, entity.DeviceId.Value, entity.Type.ToString(),
            entity.Unit, entity.EquipmentCode.Value, entity.LowerLimit, entity.UpperLimit, entity.Status.ToString(),
            entity.AssignedBatch);
    }
}
