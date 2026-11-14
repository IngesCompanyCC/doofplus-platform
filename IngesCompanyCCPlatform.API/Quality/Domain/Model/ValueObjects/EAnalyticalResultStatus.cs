namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Where an analytical result is in its review.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EAnalyticalResultStatus
{
    Pending,
    WithinSpecification,
    OutOfSpecification,
    Approved,
    Rejected
}
