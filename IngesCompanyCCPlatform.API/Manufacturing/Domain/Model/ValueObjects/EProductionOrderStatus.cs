namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     Where a production order is.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EProductionOrderStatus
{
    /// <summary>Scheduled, not approved yet.</summary>
    Planned,
    /// <summary>Approved and ready to start.</summary>
    Approved,
    /// <summary>Being executed.</summary>
    InProgress,
    /// <summary>Stopped until the issue is resolved.</summary>
    OnHold,
    /// <summary>All operations are complete.</summary>
    Completed
}
