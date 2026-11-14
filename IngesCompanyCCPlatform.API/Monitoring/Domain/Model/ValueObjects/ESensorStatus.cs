namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

/// <summary>
///     Whether a sensor is sending readings.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum ESensorStatus
{
    /// <summary>Online and sending readings.</summary>
    Active,
    /// <summary>Not sending readings.</summary>
    Offline
}
