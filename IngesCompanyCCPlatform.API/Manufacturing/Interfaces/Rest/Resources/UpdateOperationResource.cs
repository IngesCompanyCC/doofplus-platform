namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     The progress of an operation. <c>status</c> is NotStarted, InProgress, OnHold or Complete.
/// </summary>
public record UpdateOperationResource(
    int Progress,
    string Status,
    string? Reference);
