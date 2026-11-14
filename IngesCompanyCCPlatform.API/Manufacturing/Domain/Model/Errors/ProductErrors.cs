using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Errors;

/// <summary>
///     Errors of the products. Each code is the key of its message in <c>ManufacturingMessages</c>.
/// </summary>
public static class ProductErrors
{
    /// <summary>
    ///     A product with the same code already exists.
    /// </summary>
    public static readonly Error DuplicateCode = Error.Conflict("manufacturing.product.error.duplicate", "A product with the same code already exists.");

    /// <summary>
    ///     The product with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("manufacturing.product.error.notFoundById", $"Product not found for id {id}.");

    /// <summary>
    ///     The product with the code does not exist.
    /// </summary>
    /// <param name="code">The code that was looked for.</param>
    public static Error NotFoundByCode(string code) =>
        Error.NotFound("manufacturing.product.error.notFoundByCode", $"Product not found for code {code}.");
}
