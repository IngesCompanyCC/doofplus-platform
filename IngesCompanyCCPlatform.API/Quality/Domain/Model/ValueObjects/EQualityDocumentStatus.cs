namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Where a controlled document is.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EQualityDocumentStatus
{
    Draft,
    UnderReview,
    Approved,
    Obsolete
}
