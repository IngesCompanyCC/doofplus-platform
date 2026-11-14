namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     Lifecycle status of a pharmaceutical production batch.
/// </summary>
public enum BatchStatus
{
    /// <summary>Created; production has not started.</summary>
    Planned = 0,
    /// <summary>Being manufactured.</summary>
    InProgress = 1,
    /// <summary>Stopped, for example by a critical incident.</summary>
    OnHold = 2,
    /// <summary>Production finished; waiting for the QA release.</summary>
    ReleaseRequested = 3,
    /// <summary>Released by QA with its electronic signature.</summary>
    Released = 4
}
