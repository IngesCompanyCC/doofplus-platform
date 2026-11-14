namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     A product, as the Web App shows it.
/// </summary>
public record ProductResource(
    int Id,
    string Code,
    string Name,
    string DosageForm);
