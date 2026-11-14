using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Errors;

/// <summary>
///     Errors of the production orders. Each code is the key of its message in <c>ManufacturingMessages</c>.
/// </summary>
public static class ProductionOrderErrors
{
    /// <summary>
    ///     A production order with the same code already exists.
    /// </summary>
    public static readonly Error DuplicateCode = Error.Conflict("manufacturing.order.error.duplicate", "A production order with the same code already exists.");

    /// <summary>
    ///     The production order with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("manufacturing.order.error.notFoundById", $"Production order not found for id {id}.");

    /// <summary>
    ///     The production order has no operation with the sequence.
    /// </summary>
    /// <param name="sequence">The sequence that was looked for.</param>
    public static Error OperationNotFound(int sequence) =>
        Error.NotFound("manufacturing.order.error.operationNotFound", $"No operation with sequence {sequence}.");

    /// <summary>
    ///     The change is not allowed from the current status.
    /// </summary>
    /// <param name="messageKey">The key thrown by the aggregate.</param>
    public static Error InvalidState(string messageKey) =>
        Error.Conflict(messageKey, "The change is not allowed from the current status.");
}
