namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the status of the quality decision on a raw material lot.
/// </summary>
public enum EMaterialApprovalStatus
{
    Pending,
    Sampled,
    Approved,
    Rejected
}
