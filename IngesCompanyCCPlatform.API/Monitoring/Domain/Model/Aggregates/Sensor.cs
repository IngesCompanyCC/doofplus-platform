using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;

/// <summary>
///     A sensor installed on a piece of equipment, linked to its ThingsBoard device.
/// </summary>
/// <remarks>
///     The lower and upper limits define the accepted range of its readings; a reading outside them raises an alert.
/// </remarks>
public partial class Sensor : AggregateRoot
{
    private const string LimitsInvalidMessageKey = "monitoring.sensor.error.limits.invalid";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected Sensor()
    {
        Code = null!;
        DeviceId = null!;
        EquipmentCode = null!;
        Unit = string.Empty;
        AssignedBatch = string.Empty;
    }

    /// <summary>
    ///     Registers a sensor.
    /// </summary>
    /// <param name="command">The data of the sensor.</param>
    /// <exception cref="ArgumentException">Thrown when the lower limit is not below the upper limit.</exception>
    public Sensor(CreateSensorCommand command) : this()
    {
        Code = command.Code;
        DeviceId = command.DeviceId;
        Apply(command.Type, command.Unit, command.EquipmentCode, command.LowerLimit, command.UpperLimit,
            command.Status, command.AssignedBatch);
    }

    /// <summary>
    ///     The id of the sensor.
    /// </summary>
    public SensorId Id { get; }

    /// <summary>
    ///     The business code, for example T-204.
    /// </summary>
    public SensorCode Code { get; private set; }

    /// <summary>
    ///     The id of its ThingsBoard device.
    /// </summary>
    public DeviceId DeviceId { get; private set; }

    /// <summary>
    ///     What the sensor measures.
    /// </summary>
    public ESensorType Type { get; private set; }

    /// <summary>
    ///     The unit of its readings, for example °C.
    /// </summary>
    public string Unit { get; private set; }

    /// <summary>
    ///     The code of the equipment where it is installed.
    /// </summary>
    public EquipmentCode EquipmentCode { get; private set; }

    /// <summary>
    ///     The lowest accepted value.
    /// </summary>
    public decimal LowerLimit { get; private set; }

    /// <summary>
    ///     The highest accepted value.
    /// </summary>
    public decimal UpperLimit { get; private set; }

    /// <summary>
    ///     Whether the sensor is sending readings.
    /// </summary>
    public ESensorStatus Status { get; private set; }

    /// <summary>
    ///     The code of the production batch it monitors, or empty.
    /// </summary>
    public string AssignedBatch { get; private set; }

    /// <summary>
    ///     Whether the sensor is monitoring a production batch.
    /// </summary>
    public bool IsAssignedToBatch => !string.IsNullOrWhiteSpace(AssignedBatch);

    /// <summary>
    ///     Updates the sensor; its code and device do not change.
    /// </summary>
    /// <param name="command">The new data.</param>
    /// <exception cref="ArgumentException">Thrown when the lower limit is not below the upper limit.</exception>
    public void Update(UpdateSensorCommand command)
    {
        Apply(command.Type, command.Unit, command.EquipmentCode, command.LowerLimit, command.UpperLimit,
            command.Status, command.AssignedBatch);
    }

    /// <summary>
    ///     Whether a value is outside the accepted range of the sensor.
    /// </summary>
    /// <param name="value">The value of a reading.</param>
    /// <returns><c>true</c> when the value is below the lower limit or above the upper limit.</returns>
    public bool IsOutOfRange(decimal value)
    {
        return value < LowerLimit || value > UpperLimit;
    }

    private void Apply(ESensorType type, string unit, EquipmentCode equipmentCode, decimal lowerLimit,
        decimal upperLimit, ESensorStatus status, string assignedBatch)
    {
        if (lowerLimit >= upperLimit) throw new ArgumentException(LimitsInvalidMessageKey);
        Type = type;
        Unit = unit?.Trim() ?? string.Empty;
        EquipmentCode = equipmentCode;
        LowerLimit = lowerLimit;
        UpperLimit = upperLimit;
        Status = status;
        AssignedBatch = assignedBatch?.Trim() ?? string.Empty;
    }
}
