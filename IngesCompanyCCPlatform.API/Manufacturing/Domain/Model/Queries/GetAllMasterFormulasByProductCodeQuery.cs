using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;

/// <summary>
///     Query to get the master formulas of a product.
/// </summary>
public record GetAllMasterFormulasByProductCodeQuery(
    ProductCode ProductCode);
