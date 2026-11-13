namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model;

/// <summary>
///     Errors of the IoT Monitoring bounded context. Each case has its title in <c>ErrorMessages</c>
///     (<c>MonitoringError.&lt;Case&gt;</c>) and its HTTP status in <c>MonitoringActionResultAssembler</c>.
/// </summary>
public enum MonitoringError
{
    /// <summary>No error.</summary>
    None,

    /// <summary>The equipment does not exist.</summary>
    EquipmentNotFound,

    /// <summary>Another piece of equipment already has the code.</summary>
    DuplicateEquipmentCode,

    /// <summary>A sensor of the equipment is monitoring a production batch.</summary>
    EquipmentInUse,

    /// <summary>The sensor does not exist.</summary>
    SensorNotFound,

    /// <summary>Another sensor already has the code.</summary>
    DuplicateSensorCode,

    /// <summary>Another sensor is already linked to the ThingsBoard device.</summary>
    DuplicateDeviceId,

    /// <summary>The reading does not exist.</summary>
    ReadingNotFound,

    /// <summary>The alert does not exist.</summary>
    AlertNotFound,

    /// <summary>Another alert already has the code.</summary>
    DuplicateAlertCode
}
