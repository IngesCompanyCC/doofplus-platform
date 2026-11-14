namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Where a piece of evidence is in its verification.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EEvidenceStatus
{
    Pending,
    Verified,
    Rejected
}
