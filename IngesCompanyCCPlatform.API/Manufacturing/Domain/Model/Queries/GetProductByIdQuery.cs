using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;

/// <summary>
///     Query to get a product by its id.
/// </summary>
public record GetProductByIdQuery(
    ProductId ProductId);
