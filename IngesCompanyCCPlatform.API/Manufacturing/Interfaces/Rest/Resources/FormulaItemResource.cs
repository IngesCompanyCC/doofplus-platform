namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     A line of a master formula.
/// </summary>
public record FormulaItemResource(
    int Id,
    string Kind,
    string Name,
    string Role,
    string Target,
    string Tolerance,
    string Control);
