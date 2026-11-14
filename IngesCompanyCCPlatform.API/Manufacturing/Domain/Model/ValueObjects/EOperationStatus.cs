namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     Where an operation of a production order is.
/// </summary>
/// <remarks>
///     Stored as text in PascalCase, like the other enums of the project.
/// </remarks>
public enum EOperationStatus
{
    /// <summary>Not started.</summary>
    NotStarted,
    /// <summary>Being executed.</summary>
    InProgress,
    /// <summary>Stopped.</summary>
    OnHold,
    /// <summary>Finished.</summary>
    Complete
}
