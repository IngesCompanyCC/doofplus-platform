namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     Where a production order is.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EProductionOrderStatus
{
    /// <summary>Scheduled, not started.</summary>
    Planned,
    /// <summary>Being executed.</summary>
    InProgress,
    /// <summary>Stopped, for example by a deviation.</summary>
    OnHold,
    /// <summary>All its operations are complete.</summary>
    Completed
}
