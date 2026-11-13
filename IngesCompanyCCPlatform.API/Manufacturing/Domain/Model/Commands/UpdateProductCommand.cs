using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to update a product; its code does not change.
/// </summary>
public record UpdateProductCommand(
    ProductId ProductId,
    string Name,
    EDosageForm DosageForm);
