using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Repositories;

/// <summary>
///     Repository of the sensors.
/// </summary>
public interface ISensorRepository : IBaseRepository<Sensor>
{
    /// <summary>
    ///     Checks whether a sensor already has the code.
    /// </summary>
    Task<bool> ExistsByCodeAsync(SensorCode code, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks whether a sensor is already linked to the ThingsBoard device.
    /// </summary>
    Task<bool> ExistsByDeviceIdAsync(DeviceId deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds a sensor by its code, without tracking it.
    /// </summary>
    Task<Sensor?> FindByCodeAsNoTrackingAsync(SensorCode code, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Finds the sensor of a ThingsBoard device, without tracking it.
    /// </summary>
    Task<Sensor?> FindByDeviceIdAsNoTrackingAsync(DeviceId deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the sensors installed on a piece of equipment.
    /// </summary>
    Task<IEnumerable<Sensor>> FindAllByEquipmentCodeAsNoTrackingAsync(EquipmentCode equipmentCode, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Checks whether a sensor of the equipment is monitoring a production batch.
    /// </summary>
    Task<bool> ExistsAssignedToBatchByEquipmentCodeAsync(EquipmentCode equipmentCode, CancellationToken cancellationToken = default);
}
