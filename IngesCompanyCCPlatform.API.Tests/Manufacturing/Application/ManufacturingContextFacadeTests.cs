using IngesCompanyCCPlatform.API.Manufacturing.Application.Acl;
using IngesCompanyCCPlatform.API.Manufacturing.Application.QueryServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Tests.Manufacturing.Application;

/// <summary>
///     Tests of <see cref="ManufacturingContextFacade" />, with in-memory query services.
/// </summary>
public class ManufacturingContextFacadeTests
{
    private const string BatchNumber = "B-26041";

    private readonly FakeProductionBatchQueryService _batches = new();
    private readonly FakeProductionOrderQueryService _orders = new();

    private ManufacturingContextFacade Facade => new(_batches, _orders);

    private static ProductionBatch Batch() =>
        new(new CreateBatchCommand(BatchNumber, 7, 1000m, "tablets", "Line 2", "Rosa Diaz"));

    private static ProductionOrder Order() =>
        new(new CreateProductionOrderCommand(
                new ProductionOrderCode("PO-26041"), new MasterFormulaId(1), BatchNumber, 1000m, "Line 2", null, null, []),
            new ProductCode("PARA-500"));

    [Fact]
    public async Task AnExistingBatchShouldBeFoundWithItsStatusAndOrderCode()
    {
        _batches.Stored = Batch();
        _orders.Stored = Order();

        Assert.True(await Facade.ExistsBatchByNumber(BatchNumber));
        Assert.Equal(nameof(BatchStatus.Planned), await Facade.FetchBatchStatusByNumber(BatchNumber));
        Assert.Equal("PO-26041", await Facade.FetchProductionOrderCodeByBatchNumber(BatchNumber));
        Assert.Equal(7, _orders.RequestedId);
    }

    [Fact]
    public async Task AMissingBatchShouldAnswerNotFoundWithEmptyValues()
    {
        Assert.False(await Facade.ExistsBatchByNumber("B-99999"));
        Assert.Equal(string.Empty, await Facade.FetchBatchStatusByNumber("B-99999"));
        Assert.Equal(string.Empty, await Facade.FetchProductionOrderCodeByBatchNumber("B-99999"));
    }

    [Fact]
    public async Task ABatchWithoutItsOrderShouldAnswerAnEmptyOrderCode()
    {
        _batches.Stored = Batch();

        Assert.Equal(string.Empty, await Facade.FetchProductionOrderCodeByBatchNumber(BatchNumber));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("B 26041/x")]
    public async Task ABlankOrMalformedNumberShouldAnswerNotFoundWithoutThrowing(string batchNumber)
    {
        _batches.Stored = Batch();

        Assert.False(await Facade.ExistsBatchByNumber(batchNumber));
        Assert.Equal(0, _batches.Calls);
    }

    private sealed class FakeProductionBatchQueryService : IProductionBatchQueryService
    {
        public ProductionBatch? Stored { get; set; }
        public int Calls { get; private set; }

        public Task<IEnumerable<ProductionBatch>> Handle(GetAllBatchesQuery query, CancellationToken cancellationToken = default) =>
            Task.FromResult<IEnumerable<ProductionBatch>>(Stored is null ? [] : [Stored]);

        public Task<ProductionBatch?> Handle(GetBatchByIdQuery query, CancellationToken cancellationToken = default) =>
            Task.FromResult(Stored);

        public Task<ProductionBatch?> Handle(GetBatchByNumberQuery query, CancellationToken cancellationToken = default)
        {
            Calls++;
            return Task.FromResult(Stored?.BatchNumber == query.BatchNumber ? Stored : null);
        }
    }

    private sealed class FakeProductionOrderQueryService : IProductionOrderQueryService
    {
        public ProductionOrder? Stored { get; set; }
        public int? RequestedId { get; private set; }

        public Task<IEnumerable<ProductionOrder>> Handle(GetAllProductionOrdersQuery query, CancellationToken cancellationToken) =>
            Task.FromResult<IEnumerable<ProductionOrder>>(Stored is null ? [] : [Stored]);

        public Task<ProductionOrder?> Handle(GetProductionOrderByIdQuery query, CancellationToken cancellationToken)
        {
            RequestedId = query.ProductionOrderId;
            return Task.FromResult(Stored);
        }
    }
}
