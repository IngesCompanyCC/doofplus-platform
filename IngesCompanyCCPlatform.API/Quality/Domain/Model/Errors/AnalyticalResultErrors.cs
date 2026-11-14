using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;

/// <summary>
///     Errors of <c>AnalyticalResult</c>. Each code is the key of its message in <c>QualityMessages</c>.
/// </summary>
public static class AnalyticalResultErrors
{
    /// <summary>
    ///     The analytical result with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("quality.analyticalResult.error.notFoundById", $"Analytical result not found for id {id}.");
}
