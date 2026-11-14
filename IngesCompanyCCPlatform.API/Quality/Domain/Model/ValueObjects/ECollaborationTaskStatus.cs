namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Where a collaboration task is.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum ECollaborationTaskStatus
{
    Pending,
    InProgress,
    Completed,
    Cancelled
}
