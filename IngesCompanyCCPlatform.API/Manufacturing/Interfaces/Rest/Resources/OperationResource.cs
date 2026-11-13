namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     An operation of a production order.
/// </summary>
public record OperationResource(
    int Sequence,
    string Name,
    string Equipment,
    int Progress,
    string Status,
    string Reference);
