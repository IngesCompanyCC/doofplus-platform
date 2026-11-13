using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Errors;

/// <summary>
///     Errors of the master formulas. Each code is the key of its message in <c>ManufacturingMessages</c>.
/// </summary>
public static class MasterFormulaErrors
{
    /// <summary>
    ///     The product already has a formula with the same version.
    /// </summary>
    public static readonly Error DuplicateVersion = Error.Conflict("manufacturing.formula.error.duplicate", "The product already has a formula with the same version.");

    /// <summary>
    ///     The formula is not approved, so no production order can follow it.
    /// </summary>
    public static readonly Error NotApproved =
        Error.Conflict("manufacturing.formula.error.notApproved", "The master formula is not approved.");

    /// <summary>
    ///     The master formula with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("manufacturing.formula.error.notFoundById", $"Master formula not found for id {id}.");

    /// <summary>
    ///     The change is not allowed from the current status.
    /// </summary>
    /// <param name="messageKey">The key thrown by the aggregate.</param>
    public static Error InvalidState(string messageKey) =>
        Error.Conflict(messageKey, "The change is not allowed from the current status.");
}
