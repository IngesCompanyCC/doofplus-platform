namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     The quality status of a lot of raw material.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum ERawMaterialLotStatus
{
    /// <summary>Received and in quarantine until it is inspected.</summary>
    Inspection,
    /// <summary>Released for production.</summary>
    Approved,
    /// <summary>Cannot be used.</summary>
    Rejected
}
