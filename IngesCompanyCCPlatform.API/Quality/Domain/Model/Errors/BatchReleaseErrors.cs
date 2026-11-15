using IngesCompanyCCPlatform.API.Shared.Domain.Model.Errors;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;

/// <summary>
///     Errors of <c>BatchRelease</c>. Each code is the key of its message in <c>QualityMessages</c>.
/// </summary>
public static class BatchReleaseErrors
{
    /// <summary>
    ///     The batch release with the id does not exist.
    /// </summary>
    /// <param name="id">The id that was looked for.</param>
    public static Error NotFoundById(int id) =>
        Error.NotFound("quality.batchRelease.error.notFoundById", $"Batch release not found for id {id}.");
    /// <summary>
    ///     The batch already has a release request.
    /// </summary>
    public static readonly Error AlreadyRequested =
        Error.Conflict("quality.batchRelease.error.alreadyRequested", "A release request already exists for this batch.");

    /// <summary>
    ///     The release is not pending, so it cannot be approved or rejected.
    /// </summary>
    public static readonly Error NotPending =
        Error.Conflict("quality.batchRelease.error.notPending", "Only a pending release can be approved or rejected.");
}
