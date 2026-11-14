using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Errors;

/// <summary>
///     Errors of the equipment. Each code is the key of its message in <c>MonitoringMessages</c>.
/// </summary>
public static class EquipmentErrors
{
    /// <summary>
    ///     Another piece of equipment already has the code.
    /// </summary>
    public static readonly Error DuplicateCode =
        Error.Conflict("monitoring.equipment.error.duplicate", "A piece of equipment with the same code already exists.");

    /// <summary>
    ///     A sensor of the equipment is monitoring a production batch (TS07, scenario 2).
    /// </summary>
    public static readonly Error InUse =
        Error.Conflict("monitoring.equipment.error.inUse",
            "The equipment cannot be removed: one of its sensors is monitoring a production batch.");

    /// <summary>
    ///     The equipment with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("monitoring.equipment.error.notFoundById", $"Equipment not found for id {id}.");

    /// <summary>
    ///     The equipment with the code does not exist.
    /// </summary>
    /// <param name="code">The code that was looked for.</param>
    public static Error NotFoundByCode(string code) =>
        Error.NotFound("monitoring.equipment.error.notFoundByCode", $"Equipment not found for code {code}.");
}
