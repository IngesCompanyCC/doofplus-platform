namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Where a regulatory report is.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum ERegulatoryReportStatus
{
    Draft,
    UnderReview,
    Approved,
    Submitted,
    Rejected
}
