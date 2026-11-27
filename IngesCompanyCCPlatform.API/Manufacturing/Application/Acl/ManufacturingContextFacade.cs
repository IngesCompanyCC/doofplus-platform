using IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Acl;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.Acl;

/// <summary>
///     Answers the questions of other bounded contexts about the production batches with the Manufacturing query services.
/// </summary>
public class ManufacturingContextFacade(
    IProductionBatchQueryService productionBatchQueryService,
    IProductionOrderQueryService productionOrderQueryService) : IManufacturingContextFacade
{
    /// <inheritdoc />
    public async Task<bool> ExistsBatchByNumber(string batchNumber, CancellationToken cancellationToken = default)
    {
        return await FindBatch(batchNumber, cancellationToken) is not null;
    }

    /// <inheritdoc />
    public async Task<string> FetchBatchStatusByNumber(string batchNumber, CancellationToken cancellationToken = default)
    {
        var batch = await FindBatch(batchNumber, cancellationToken);
        return batch?.Status.ToString() ?? string.Empty;
    }

    /// <inheritdoc />
    public async Task<string> FetchProductionOrderCodeByBatchNumber(string batchNumber, CancellationToken cancellationToken = default)
    {
        var batch = await FindBatch(batchNumber, cancellationToken);
        if (batch is null) return string.Empty;

        var order = await productionOrderQueryService.Handle(
            new GetProductionOrderByIdQuery(batch.ProductionOrderId), cancellationToken);
        return order?.Code.Value ?? string.Empty;
    }

    private async Task<ProductionBatch?> FindBatch(string batchNumber, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(batchNumber)) return null;

        GetBatchByNumberQuery query;
        try
        {
            query = new GetBatchByNumberQuery(batchNumber);
        }
        catch (ArgumentException)
        {
            // A malformed number cannot belong to any batch.
            return null;
        }

        return await productionBatchQueryService.Handle(query, cancellationToken);
    }
}
