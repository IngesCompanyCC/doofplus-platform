using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to write a new version of the master formula of a product; it starts as Draft.
/// </summary>
public record CreateMasterFormulaCommand(
    ProductCode ProductCode,
    string Version,
    decimal BatchSize,
    IReadOnlyList<FormulaItemData> Items);
