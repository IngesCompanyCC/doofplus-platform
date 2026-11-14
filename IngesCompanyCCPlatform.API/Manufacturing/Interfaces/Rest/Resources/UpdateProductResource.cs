namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     New data of a product; its code does not change.
/// </summary>
public record UpdateProductResource(
    string Name,
    string DosageForm);
