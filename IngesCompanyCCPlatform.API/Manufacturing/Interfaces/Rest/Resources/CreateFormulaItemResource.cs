namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     A line of a new master formula. <c>kind</c> is Component or Parameter.
/// </summary>
public record CreateFormulaItemResource(
    string Kind,
    string Name,
    string? Role,
    string? Target,
    string? Tolerance,
    string? Control);
