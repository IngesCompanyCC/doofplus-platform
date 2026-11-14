namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

/// <summary>
///     What a sensor measures.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum ESensorType
{
    /// <summary>Temperature.</summary>
    Temperature,
    /// <summary>Relative humidity.</summary>
    Humidity,
    /// <summary>Pressure.</summary>
    Pressure
}
