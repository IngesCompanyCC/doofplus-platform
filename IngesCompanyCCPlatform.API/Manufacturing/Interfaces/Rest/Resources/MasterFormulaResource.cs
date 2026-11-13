namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     A master formula with its lines.
/// </summary>
public record MasterFormulaResource(
    int Id,
    string ProductCode,
    string Version,
    string Status,
    string ApprovedBy,
    DateOnly? ApprovedAt,
    decimal BatchSize,
    IEnumerable<FormulaItemResource> Items);
