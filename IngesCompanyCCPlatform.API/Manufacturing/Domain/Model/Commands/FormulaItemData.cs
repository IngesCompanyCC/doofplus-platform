using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     A line of a master formula, as it comes in a command.
/// </summary>
public record FormulaItemData(
    EFormulaItemKind Kind,
    string Name,
    string Role,
    string Target,
    string Tolerance,
    string Control);
