namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

/// <summary>
///     Where an alert is in its handling.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EAlertStatus
{
    /// <summary>Raised and not handled yet.</summary>
    Open,
    /// <summary>Someone took charge of it.</summary>
    Acknowledged,
    /// <summary>Being investigated, for example with a deviation.</summary>
    UnderInvestigation
}
