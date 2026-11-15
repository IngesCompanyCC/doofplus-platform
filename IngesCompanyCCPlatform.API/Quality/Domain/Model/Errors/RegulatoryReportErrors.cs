using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;

/// <summary>
///     Errors of <c>RegulatoryReport</c>. Each code is the key of its message in <c>QualityMessages</c>.
/// </summary>
public static class RegulatoryReportErrors
{
    /// <summary>
    ///     The regulatory report with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("quality.regulatoryReport.error.notFoundById", $"Regulatory report not found for id {id}.");

    /// <summary>
    ///     Another regulatory report already has the code.
    /// </summary>
    public static readonly Error DuplicateCode =
        Error.Conflict("quality.regulatoryReport.error.duplicate", "A regulatory report with the same code already exists.");
}
