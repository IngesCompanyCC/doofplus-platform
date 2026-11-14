using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Errors;

/// <summary>
///     Errors of the sensors. Each code is the key of its message in <c>MonitoringMessages</c>.
/// </summary>
public static class SensorErrors
{
    /// <summary>
    ///     Another sensor already has the code.
    /// </summary>
    public static readonly Error DuplicateCode =
        Error.Conflict("monitoring.sensor.error.duplicate", "A sensor with the same code already exists.");

    /// <summary>
    ///     Another sensor is already linked to the ThingsBoard device.
    /// </summary>
    public static readonly Error DuplicateDeviceId =
        Error.Conflict("monitoring.sensor.error.deviceId.duplicate", "Another sensor is already linked to the device.");

    /// <summary>
    ///     The sensor with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("monitoring.sensor.error.notFoundById", $"Sensor not found for id {id}.");

    /// <summary>
    ///     The sensor with the code does not exist.
    /// </summary>
    /// <param name="code">The code that was looked for.</param>
    public static Error NotFoundByCode(string code) =>
        Error.NotFound("monitoring.sensor.error.notFoundByCode", $"Sensor not found for code {code}.");

    /// <summary>
    ///     No sensor is linked to the ThingsBoard device.
    /// </summary>
    /// <param name="deviceId">The device that sent the reading.</param>
    public static Error NotFoundByDeviceId(string deviceId) =>
        Error.NotFound("monitoring.sensor.error.notFoundByDeviceId", $"No sensor is linked to the device {deviceId}.");
}
