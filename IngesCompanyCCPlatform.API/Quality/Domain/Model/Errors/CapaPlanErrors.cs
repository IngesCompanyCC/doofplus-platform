using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;

/// <summary>
///     Errors of <c>CapaPlan</c>. Each code is the key of its message in <c>QualityMessages</c>.
/// </summary>
public static class CapaPlanErrors
{
    /// <summary>
    ///     The capa plan with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("quality.capaPlan.error.notFoundById", $"Capa plan not found for id {id}.");
}
