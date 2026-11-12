using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;

/// <summary>
///     Query to retrieve a production batch by its unique batch number.
/// </summary>
public record GetBatchByNumberQuery(BatchNumber BatchNumber)
{
    public GetBatchByNumberQuery(string batchNumber) : this(new BatchNumber(batchNumber))
    {
    }
}
