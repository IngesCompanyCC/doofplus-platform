namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     Whether Quality took the samples of a lot.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum ESamplingStatus
{
    /// <summary>Samples requested.</summary>
    Requested,
    /// <summary>Samples taken.</summary>
    Done
}
