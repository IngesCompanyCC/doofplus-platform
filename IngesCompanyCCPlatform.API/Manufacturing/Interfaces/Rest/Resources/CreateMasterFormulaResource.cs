namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Data to write a new version of the formula of a product; it starts as Draft.
/// </summary>
public record CreateMasterFormulaResource(
    string ProductCode,
    string Version,
    decimal BatchSize,
    IReadOnlyList<CreateFormulaItemResource>? Items);
