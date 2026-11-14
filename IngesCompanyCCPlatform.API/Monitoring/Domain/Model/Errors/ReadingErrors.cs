using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Errors;

/// <summary>
///     Errors of the readings. Each code is the key of its message in <c>MonitoringMessages</c>.
/// </summary>
public static class ReadingErrors
{
    /// <summary>
    ///     The reading with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("monitoring.reading.error.notFoundById", $"Reading not found for id {id}.");
}
