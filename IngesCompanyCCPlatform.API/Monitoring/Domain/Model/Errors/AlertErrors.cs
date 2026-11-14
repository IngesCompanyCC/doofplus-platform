using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Errors;

/// <summary>
///     Errors of the alerts. Each code is the key of its message in <c>MonitoringMessages</c>.
/// </summary>
public static class AlertErrors
{
    /// <summary>
    ///     Another alert already has the code.
    /// </summary>
    public static readonly Error DuplicateCode =
        Error.Conflict("monitoring.alert.error.duplicate", "An alert with the same code already exists.");

    /// <summary>
    ///     The alert with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("monitoring.alert.error.notFoundById", $"Alert not found for id {id}.");
}
