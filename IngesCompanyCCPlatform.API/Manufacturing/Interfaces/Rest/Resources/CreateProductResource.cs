namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Data to register a product. <c>dosageForm</c> is Tablet, Capsule, Syrup, Injectable or Cream.
/// </summary>
public record CreateProductResource(
    string Code,
    string Name,
    string DosageForm);
