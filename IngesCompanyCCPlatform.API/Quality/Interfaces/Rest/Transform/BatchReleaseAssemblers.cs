using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>BatchRelease</c> from the resources.
/// </summary>
public static class BatchReleaseCommandFromResourceAssembler
{


    /// <summary>
    ///     Builds the command to create a batch release.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateBatchReleaseCommand ToCommandFromResource(CreateBatchReleaseResource resource)
    {
        return new CreateBatchReleaseCommand(
            resource.BatchId,
            resource.BatchCode,
            resource.RequestedBy);
    }

    /// <summary>
    ///     Builds the command to approve a release.
    /// </summary>
    public static ApproveBatchReleaseCommand ToCommandFromResource(int releaseId, ApproveBatchReleaseResource resource)
    {
        return new ApproveBatchReleaseCommand(releaseId, resource.Decision, resource.SignatureReference, resource.SignedBy);
    }

    /// <summary>
    ///     Builds the command to reject a release.
    /// </summary>
    public static RejectBatchReleaseCommand ToCommandFromResource(int releaseId, RejectBatchReleaseResource resource)
    {
        return new RejectBatchReleaseCommand(releaseId, resource.Decision, resource.SignedBy);
    }
}

/// <summary>
///     Builds the resource of a batch release.
/// </summary>
public static class BatchReleaseResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The batch release.</param>
    /// <returns>The resource.</returns>
    public static BatchReleaseResource ToResourceFromEntity(BatchRelease entity)
    {
        return new BatchReleaseResource(
            entity.Id.Value,
            entity.BatchId,
            entity.BatchCode,
            entity.RequestedBy,
            entity.Status.ToString(),
            entity.Decision,
            entity.SignatureReference,
            entity.SignedBy,
            entity.SignedAt is null ? null : DateTime.SpecifyKind(entity.SignedAt.Value, DateTimeKind.Utc),
            entity.CreatedAt);
    }
}
