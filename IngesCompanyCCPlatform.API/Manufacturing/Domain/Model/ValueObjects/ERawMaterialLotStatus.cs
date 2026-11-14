namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     The quality status of a lot of raw material.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum ERawMaterialLotStatus
{
    /// <summary>Received and waiting for sampling.</summary>
    Quarantine,
    /// <summary>Samples are being taken.</summary>
    Sampling,
    /// <summary>Under quality inspection.</summary>
    Inspection,
    /// <summary>Released for production.</summary>
    Approved,
    /// <summary>Rejected by Quality.</summary>
    Rejected
}
