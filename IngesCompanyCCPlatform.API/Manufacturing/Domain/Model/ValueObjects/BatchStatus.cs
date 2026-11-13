namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     Lifecycle status of a pharmaceutical production batch.
/// </summary>
public enum BatchStatus
{
    Planned = 0,
    InProgress = 1,
    OnHold = 2,
    Closed = 3
}
