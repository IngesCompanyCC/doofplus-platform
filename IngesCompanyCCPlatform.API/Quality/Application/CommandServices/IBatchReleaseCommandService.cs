using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Quality.Application.CommandServices;

/// <summary>
///     Command service of <see cref="BatchRelease" />.
/// </summary>
public interface IBatchReleaseCommandService
{
    /// <summary>
    ///     Creates a batch release.
    /// </summary>
    Task<Result<BatchRelease>> Handle(CreateBatchReleaseCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Approves a pending release; fails if it does not exist or is not pending.
    /// </summary>
    Task<Result<BatchRelease>> Handle(ApproveBatchReleaseCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Rejects a pending release; fails if it does not exist or is not pending.
    /// </summary>
    Task<Result<BatchRelease>> Handle(RejectBatchReleaseCommand command, CancellationToken cancellationToken);
}
