namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Where the release of a batch is.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EBatchReleaseStatus
{
    Pending,
    Approved,
    Rejected,
    Released
}
