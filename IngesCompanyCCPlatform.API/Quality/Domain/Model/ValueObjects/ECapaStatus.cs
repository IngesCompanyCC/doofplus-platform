namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Where a CAPA plan or action is.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum ECapaStatus
{
    Open,
    InProgress,
    PendingVerification,
    Completed,
    Cancelled
}
