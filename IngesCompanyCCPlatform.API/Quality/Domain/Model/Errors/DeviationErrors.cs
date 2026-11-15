using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;

/// <summary>
///     Errors of <c>Deviation</c>. Each code is the key of its message in <c>QualityMessages</c>.
/// </summary>
public static class DeviationErrors
{
    /// <summary>
    ///     The deviation with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("quality.deviation.error.notFoundById", $"Deviation not found for id {id}.");

    /// <summary>
    ///     Another deviation already has the code.
    /// </summary>
    public static readonly Error DuplicateCode =
        Error.Conflict("quality.deviation.error.duplicate", "A deviation with the same code already exists.");
}
