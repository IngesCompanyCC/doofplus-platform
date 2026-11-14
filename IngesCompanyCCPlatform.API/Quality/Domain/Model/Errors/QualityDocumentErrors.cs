using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;

/// <summary>
///     Errors of <c>QualityDocument</c>. Each code is the key of its message in <c>QualityMessages</c>.
/// </summary>
public static class QualityDocumentErrors
{
    /// <summary>
    ///     The quality document with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("quality.qualityDocument.error.notFoundById", $"Quality document not found for id {id}.");
    /// <summary>
    ///     Another quality document already has the code.
    /// </summary>
    public static readonly Error DuplicateCode =
        Error.Conflict("quality.qualityDocument.error.duplicate", "A quality document with the same code already exists.");
}
