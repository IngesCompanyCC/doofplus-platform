namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Where a traceability gap is.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum ETraceabilityGapStatus
{
    Open,
    UnderReview,
    Resolved
}
