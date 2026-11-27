namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Acl;

/// <summary>
///     Lets other bounded contexts read the production batches without knowing the Manufacturing model.
/// </summary>
/// <remarks>
///     Quality and IoT Monitoring only keep the batch number, so every method looks the batch up by it.
///     A blank, malformed or unknown batch number is answered as a missing batch, never with an exception.
/// </remarks>
public interface IManufacturingContextFacade
{
    /// <summary>
    ///     Checks whether a production batch has the batch number.
    /// </summary>
    /// <param name="batchNumber">The batch number, for example B-26041.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns><c>true</c> when the batch exists.</returns>
    Task<bool> ExistsBatchByNumber(string batchNumber, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the status of the production batch, for example <c>InProgress</c> or <c>OnHold</c>.
    /// </summary>
    /// <param name="batchNumber">The batch number.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The name of the status, or an empty string when the batch does not exist.</returns>
    Task<string> FetchBatchStatusByNumber(string batchNumber, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the code of the production order of the batch, for example PO-26041.
    /// </summary>
    /// <param name="batchNumber">The batch number.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The code of the order, or an empty string when the batch or its order does not exist.</returns>
    Task<string> FetchProductionOrderCodeByBatchNumber(string batchNumber, CancellationToken cancellationToken = default);
}
