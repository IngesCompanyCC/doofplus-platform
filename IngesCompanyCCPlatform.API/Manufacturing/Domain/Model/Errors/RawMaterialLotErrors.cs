using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Errors;

/// <summary>
///     Errors of the lots of raw material. Each code is the key of its message in <c>ManufacturingMessages</c>.
/// </summary>
public static class RawMaterialLotErrors
{
    /// <summary>
    ///     A lot with the same code already exists.
    /// </summary>
    public static readonly Error DuplicateCode = Error.Conflict("manufacturing.lot.error.duplicate", "A lot with the same code already exists.");

    /// <summary>
    ///     The lot with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("manufacturing.lot.error.notFoundById", $"Raw material lot not found for id {id}.");

    /// <summary>
    ///     The change is not allowed from the current status.
    /// </summary>
    /// <param name="messageKey">The key thrown by the aggregate.</param>
    public static Error InvalidState(string messageKey) =>
        Error.Conflict(messageKey, "The change is not allowed from the current status.");
}
