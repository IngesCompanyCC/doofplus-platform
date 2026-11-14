using IngesCompanyCCPlatform.API.Quality.Application.CommandServices;
using IngesCompanyCCPlatform.API.Quality.Domain.Model;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Quality.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.CommandServices;

/// <summary>
///     Command service of <see cref="BatchRelease" />.
/// </summary>
public class BatchReleaseCommandService(
    IBatchReleaseRepository batchReleaseRepository,
    IAuditEventRepository auditEventRepository,
    IUnitOfWork unitOfWork) : IBatchReleaseCommandService
{
    /// <inheritdoc />
    public async Task<Result<BatchRelease>> Handle(CreateBatchReleaseCommand command, CancellationToken cancellationToken)
    {
        if (await batchReleaseRepository.ExistsByBatchIdAsync(command.BatchId, cancellationToken))
            return Result<BatchRelease>.Failure(BatchReleaseErrors.AlreadyRequested, QualityError.BatchReleaseAlreadyRequested);

        var batchRelease = new BatchRelease(command);
        await batchReleaseRepository.AddAsync(batchRelease, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        await auditEventRepository.AddAsync(new AuditEvent(new CreateAuditEventCommand(
            nameof(BatchRelease), batchRelease.Id.Value, "Requested",
            $"Release of batch {batchRelease.BatchCode} requested.", batchRelease.RequestedBy)), cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<BatchRelease>.Success(batchRelease);
    }

    /// <inheritdoc />
    public async Task<Result<BatchRelease>> Handle(ApproveBatchReleaseCommand command, CancellationToken cancellationToken)
    {
        var release = await batchReleaseRepository.FindByIdAsync(command.ReleaseId, cancellationToken);
        if (release is null)
            return Result<BatchRelease>.Failure(BatchReleaseErrors.NotFoundById(command.ReleaseId), QualityError.BatchReleaseNotFound);
        if (!release.Approve(command))
            return Result<BatchRelease>.Failure(BatchReleaseErrors.NotPending, QualityError.BatchReleaseStateConflict);

        await auditEventRepository.AddAsync(new AuditEvent(new CreateAuditEventCommand(
            nameof(BatchRelease), release.Id.Value, "Approved",
            $"Batch {release.BatchCode} approved: {release.Decision}", release.SignedBy)), cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<BatchRelease>.Success(release);
    }

    /// <inheritdoc />
    public async Task<Result<BatchRelease>> Handle(RejectBatchReleaseCommand command, CancellationToken cancellationToken)
    {
        var release = await batchReleaseRepository.FindByIdAsync(command.ReleaseId, cancellationToken);
        if (release is null)
            return Result<BatchRelease>.Failure(BatchReleaseErrors.NotFoundById(command.ReleaseId), QualityError.BatchReleaseNotFound);
        if (!release.Reject(command))
            return Result<BatchRelease>.Failure(BatchReleaseErrors.NotPending, QualityError.BatchReleaseStateConflict);

        await auditEventRepository.AddAsync(new AuditEvent(new CreateAuditEventCommand(
            nameof(BatchRelease), release.Id.Value, "Rejected",
            $"Batch {release.BatchCode} rejected: {release.Decision}", release.SignedBy)), cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<BatchRelease>.Success(release);
    }
}
