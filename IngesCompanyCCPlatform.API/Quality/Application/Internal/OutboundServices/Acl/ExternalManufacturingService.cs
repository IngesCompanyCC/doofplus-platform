using IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Acl;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.OutboundServices.Acl;

/// <summary>
///     Reads the production batches of Manufacturing for Quality, through its context facade.
/// </summary>
/// <remarks>
///     Quality only keeps the batch and order codes as text, so it never uses the Manufacturing model.
/// </remarks>
public class ExternalManufacturingService(IManufacturingContextFacade manufacturingContextFacade)
{
    /// <summary>
    ///     Gets the code of the production order of the batch.
    /// </summary>
    /// <param name="batchNumber">The batch number.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The order code, or an empty string when Manufacturing does not know the batch.</returns>
    public Task<string> FetchProductionOrderCodeByBatchNumber(string batchNumber, CancellationToken cancellationToken = default)
    {
        return manufacturingContextFacade.FetchProductionOrderCodeByBatchNumber(batchNumber, cancellationToken);
    }
}
