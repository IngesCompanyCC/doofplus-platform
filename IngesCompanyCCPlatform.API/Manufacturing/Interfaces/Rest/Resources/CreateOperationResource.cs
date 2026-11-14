namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     An operation of a new production order.
/// </summary>
public record CreateOperationResource(
    int Sequence,
    string Name,
    string? Equipment);
