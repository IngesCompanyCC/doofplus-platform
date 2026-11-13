using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to register a product.
/// </summary>
public record CreateProductCommand(
    ProductCode Code,
    string Name,
    EDosageForm DosageForm);
