namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the status of the review of a production batch.
/// </summary>
public enum EReviewStatus
{
    InQuarantine,
    UnderEvaluation,
    Released,
    Rejected
}
