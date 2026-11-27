using IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Tests.Manufacturing.Application;

/// <summary>
///     Tests of the registration and the closing of a batch in <see cref="ProductionBatchCommandService" /> (US14,
///     US16), with in-memory test doubles.
/// </summary>
public class ProductionBatchCommandServiceTests
{
    private readonly InMemoryProductionBatchRepository _batches = new();
    private readonly CountingUnitOfWork _unitOfWork = new();

    private ProductionBatchCommandService Service => new(_batches, _unitOfWork);

    private static CreateBatchCommand Create(string batchNumber = "B-26041") =>
        new(batchNumber, 7, 1000m, "tablets", "Line 2", "Rosa Diaz");

    [Fact]
    public async Task ANewBatchIsRegisteredAsPlanned()
    {
        var result = await Service.Handle(Create());

        Assert.True(result.IsSuccess);
        Assert.Equal(BatchStatus.Planned, Assert.Single(_batches.Items).Status);
        Assert.Equal(1, _unitOfWork.Completed);
    }

    [Fact]
    public async Task ARepeatedBatchNumberIsRejected()
    {
        await Service.Handle(Create());

        var result = await Service.Handle(Create());

        Assert.Equal(ManufacturingError.DuplicateBatchNumber, result.Error);
        Assert.Single(_batches.Items);
        Assert.Equal(1, _unitOfWork.Completed);
    }

    [Fact]
    public async Task ClosingAnUnknownBatchIsNotFound()
    {
        var result = await Service.Handle(new CloseBatchCommand(99, 980m, "tablets"));

        Assert.Equal(ManufacturingError.BatchNotFound, result.Error);
    }

    [Fact]
    public async Task APlannedBatchCannotBeClosed()
    {
        await Service.Handle(Create());

        var result = await Service.Handle(new CloseBatchCommand(0, 980m, "tablets"));

        Assert.Equal(ManufacturingError.InvalidBatchState, result.Error);
        Assert.Equal(BatchStatus.Planned, _batches.Items[0].Status);
    }

    [Fact]
    public async Task ABatchInProgressIsClosedAndRequestsItsRelease()
    {
        await Service.Handle(Create());
        _batches.Items[0].Start();

        var result = await Service.Handle(new CloseBatchCommand(0, 980m, "tablets"));

        Assert.True(result.IsSuccess);
        Assert.Equal(BatchStatus.ReleaseRequested, result.Value!.Status);
    }

    private sealed class InMemoryProductionBatchRepository : IProductionBatchRepository
    {
        public List<ProductionBatch> Items { get; } = [];

        public Task AddAsync(ProductionBatch entity, CancellationToken cancellationToken = default)
        {
            Items.Add(entity);
            return Task.CompletedTask;
        }

        public Task<ProductionBatch?> FindByIdAsync(int id, CancellationToken cancellationToken = default) =>
            Task.FromResult(Items.FirstOrDefault(batch => batch.Id == id));

        public Task<ProductionBatch?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default) =>
            FindByIdAsync(id, cancellationToken);

        public void Update(ProductionBatch entity)
        {
        }

        public void Remove(ProductionBatch entity) => Items.Remove(entity);

        public Task<IEnumerable<ProductionBatch>> ListAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IEnumerable<ProductionBatch>>(Items);

        public Task<IEnumerable<ProductionBatch>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default) =>
            ListAsync(cancellationToken);

        public Task<ProductionBatch?> FindByBatchNumberAsync(BatchNumber batchNumber, CancellationToken cancellationToken = default) =>
            Task.FromResult(Items.FirstOrDefault(batch => batch.BatchNumber == batchNumber));

        public Task<bool> ExistsByBatchNumberAsync(BatchNumber batchNumber, CancellationToken cancellationToken = default) =>
            Task.FromResult(Items.Any(batch => batch.BatchNumber == batchNumber));

        public Task<IEnumerable<ProductionBatch>> FindByStatusAsync(BatchStatus status, CancellationToken cancellationToken = default) =>
            Task.FromResult(Items.Where(batch => batch.Status == status));
    }

    private sealed class CountingUnitOfWork : IUnitOfWork
    {
        public int Completed { get; private set; }

        public Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            Completed++;
            return Task.CompletedTask;
        }
    }
}
