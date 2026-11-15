namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Task status values of the Quality &amp; Compliance bounded context.
/// </summary>
public enum ETaskStatus
{
    ToDo,
    Open,
    Pending,
    InReview,
    Ready,
    Done
}
