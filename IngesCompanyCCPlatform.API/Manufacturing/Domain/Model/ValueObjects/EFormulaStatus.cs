namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     Where a master formula is in its life cycle.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EFormulaStatus
{
    /// <summary>Being written; cannot be used yet.</summary>
    Draft,
    /// <summary>Sent to Quality and waiting for approval.</summary>
    Pending,
    /// <summary>Approved by Quality; production orders can follow it.</summary>
    Approved,
    /// <summary>Replaced by a newer approved version; kept for traceability.</summary>
    Locked
}
