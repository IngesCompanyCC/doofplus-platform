using IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Tests.Manufacturing.Application;

/// <summary>
///     Tests of the rejection use case of <see cref="ProductionOrderCommandService" />, with in-memory test doubles.
/// </summary>
public class ProductionOrderCommandServiceTests
{
    private readonly InMemoryProductionOrderRepository _orders = new();
    private readonly CountingUnitOfWork _unitOfWork = new();

    private ProductionOrderCommandService Service => new(_orders, new UnusedMasterFormulaRepository(), _unitOfWork);

    private ProductionOrder StoredOrder()
    {
        var order = new ProductionOrder(
            new CreateProductionOrderCommand(new ProductionOrderCode("PO-26041"), new MasterFormulaId(1), "B-26041",
                1000m, "Line 2", null, null, []),
            new ProductCode("PARA-500"));
        _orders.Stored = order;
        return order;
    }

    [Fact]
    public async Task APlannedOrderIsRejectedAndSaved()
    {
        StoredOrder();

        var result = await Service.Handle(new RejectProductionOrderCommand(0, "Wrong master formula."), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(EProductionOrderStatus.Rejected, result.Value!.Status);
        Assert.Equal("Wrong master formula.", result.Value.RejectionReason);
        Assert.Equal(1, _unitOfWork.Completed);
    }

    [Fact]
    public async Task ARejectionWithoutReasonIsAnInvalidOperation()
    {
        StoredOrder();

        var result = await Service.Handle(new RejectProductionOrderCommand(0, " "), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(ManufacturingError.InvalidOperation, result.Error);
        Assert.Equal("manufacturing.order.error.rejectionReason.notBlank", result.DomainError.Code);
        Assert.Equal(0, _unitOfWork.Completed);
    }

    [Fact]
    public async Task AnApprovedOrderCannotBeRejected()
    {
        StoredOrder().ChangeStatus(EProductionOrderStatus.Approved);

        var result = await Service.Handle(new RejectProductionOrderCommand(0, "Late rejection."), CancellationToken.None);

        Assert.Equal(ManufacturingError.InvalidProductionOrderState, result.Error);
        Assert.Equal(0, _unitOfWork.Completed);
    }

    [Fact]
    public async Task AnUnknownOrderIsNotFound()
    {
        var result = await Service.Handle(new RejectProductionOrderCommand(99, "Any reason."), CancellationToken.None);

        Assert.Equal(ManufacturingError.ProductionOrderNotFound, result.Error);
    }

    private sealed class InMemoryProductionOrderRepository : IProductionOrderRepository
    {
        public ProductionOrder? Stored { get; set; }

        public Task AddAsync(ProductionOrder entity, CancellationToken cancellationToken = default)
        {
            Stored = entity;
            return Task.CompletedTask;
        }

        public Task<ProductionOrder?> FindByIdAsync(int id, CancellationToken cancellationToken = default) =>
            Task.FromResult(Stored is not null && Stored.Id.Value == id ? Stored : null);

        public Task<ProductionOrder?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default) =>
            FindByIdAsync(id, cancellationToken);

        public void Update(ProductionOrder entity) => Stored = entity;

        public void Remove(ProductionOrder entity) => Stored = null;

        public Task<IEnumerable<ProductionOrder>> ListAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IEnumerable<ProductionOrder>>(Stored is null ? [] : [Stored]);

        public Task<IEnumerable<ProductionOrder>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default) =>
            ListAsync(cancellationToken);

        public Task<bool> ExistsByCodeAsync(ProductionOrderCode code, CancellationToken cancellationToken = default) =>
            Task.FromResult(Stored is not null && Stored.Code == code);
    }

    /// <summary>The rejection does not read master formulas.</summary>
    private sealed class UnusedMasterFormulaRepository : IMasterFormulaRepository
    {
        public Task AddAsync(MasterFormula entity, CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public Task<MasterFormula?> FindByIdAsync(int id, CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public Task<MasterFormula?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public void Update(MasterFormula entity) => throw new NotSupportedException();
        public void Remove(MasterFormula entity) => throw new NotSupportedException();
        public Task<IEnumerable<MasterFormula>> ListAsync(CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public Task<IEnumerable<MasterFormula>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public Task<bool> ExistsByProductCodeAndVersionAsync(ProductCode productCode, string version, CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public Task<IEnumerable<MasterFormula>> FindAllByProductCodeAsync(ProductCode productCode, CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public Task<IEnumerable<MasterFormula>> FindAllByProductCodeAsNoTrackingAsync(ProductCode productCode, CancellationToken cancellationToken = default) => throw new NotSupportedException();
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
