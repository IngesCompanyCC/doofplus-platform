namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Acl;

/// <summary>
///     Facade of the Quality &amp; Compliance bounded context for the other bounded contexts.
/// </summary>
/// <remarks>
///     Manufacturing uses it to request the approval of a raw material lot and the quarantine of a batch.
///     The decisions come back as the <c>MaterialApprovalDecidedEvent</c> and <c>BatchReleaseDecidedEvent</c> domain events.
/// </remarks>
public interface IQualityContextFacade
{
    /// <summary>
    ///     Requests the quality decision on a raw material lot.
    /// </summary>
    /// <param name="rawMaterialLotId">The id of the raw material lot.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The id of the material approval if successful, 0 otherwise.</returns>
    Task<int> RequestMaterialApproval(int rawMaterialLotId, CancellationToken cancellationToken);

    /// <summary>
    ///     Puts a production batch in quarantine and opens its review.
    /// </summary>
    /// <param name="productionBatchId">The id of the production batch.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The id of the batch review if successful, 0 otherwise.</returns>
    Task<int> StartBatchQuarantine(int productionBatchId, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets the status of the review of a production batch.
    /// </summary>
    /// <param name="productionBatchId">The id of the production batch.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The status, for example <c>InQuarantine</c> or <c>Released</c>, or an empty string when there is no review.</returns>
    Task<string> FetchBatchReviewStatus(int productionBatchId, CancellationToken cancellationToken);
}
