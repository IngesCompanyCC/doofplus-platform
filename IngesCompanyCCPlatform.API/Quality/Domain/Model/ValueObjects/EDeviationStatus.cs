namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Where a deviation is in its investigation.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EDeviationStatus
{
    Open,
    UnderInvestigation,
    CapaRequired,
    Dispositioned,
    Closed
}
