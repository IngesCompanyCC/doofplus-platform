namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     An operation of a production order, as it comes in a command.
/// </summary>
public record OperationData(
    int Sequence,
    string Name,
    string Equipment);
