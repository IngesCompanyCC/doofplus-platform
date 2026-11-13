namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

/// <summary>
///     How serious an alert is.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EAlertSeverity
{
    /// <summary>Needs attention.</summary>
    Warning,
    /// <summary>Needs an immediate response.</summary>
    Critical
}
