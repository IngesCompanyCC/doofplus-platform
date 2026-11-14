using IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Errors;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.Internal.CommandServices;

/// <summary>
///     Command service of the production orders.
/// </summary>
public class ProductionOrderCommandService(
    IProductionOrderRepository productionOrderRepository,
    IMasterFormulaRepository masterFormulaRepository,
    IUnitOfWork unitOfWork) : IProductionOrderCommandService
{
    /// <inheritdoc />
    public async Task<Result<ProductionOrder>> Handle(CreateProductionOrderCommand command,
        CancellationToken cancellationToken)
    {
        if (await productionOrderRepository.ExistsByCodeAsync(command.Code, cancellationToken))
            return Result<ProductionOrder>.Failure(ProductionOrderErrors.DuplicateCode,
                ManufacturingError.DuplicateProductionOrderCode);

        var formula = await masterFormulaRepository.FindByIdAsNoTrackingAsync(command.MasterFormulaId,
            cancellationToken);
        if (formula is null)
            return Result<ProductionOrder>.Failure(MasterFormulaErrors.NotFoundById(command.MasterFormulaId),
                ManufacturingError.MasterFormulaNotFound);
        if (!formula.IsApproved)
            return Result<ProductionOrder>.Failure(MasterFormulaErrors.NotApproved,
                ManufacturingError.FormulaNotApproved);

        var order = new ProductionOrder(command, formula.ProductCode);
        await productionOrderRepository.AddAsync(order, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<ProductionOrder>.Success(order);
    }

    /// <inheritdoc />
    public async Task<Result<ProductionOrder>> Handle(ChangeProductionOrderStatusCommand command,
        CancellationToken cancellationToken)
    {
        var order = await productionOrderRepository.FindByIdAsync(command.ProductionOrderId, cancellationToken);
        if (order is null)
            return Result<ProductionOrder>.Failure(ProductionOrderErrors.NotFoundById(command.ProductionOrderId),
                ManufacturingError.ProductionOrderNotFound);

        try
        {
            order.ChangeStatus(command.Status);
        }
        catch (InvalidOperationException exception)
        {
            return Result<ProductionOrder>.Failure(ProductionOrderErrors.InvalidState(exception.Message),
                ManufacturingError.InvalidProductionOrderState);
        }

        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<ProductionOrder>.Success(order);
    }

    /// <inheritdoc />
    public async Task<Result<ProductionOrder>> Handle(UpdateOperationCommand command,
        CancellationToken cancellationToken)
    {
        var order = await productionOrderRepository.FindByIdAsync(command.ProductionOrderId, cancellationToken);
        if (order is null)
            return Result<ProductionOrder>.Failure(ProductionOrderErrors.NotFoundById(command.ProductionOrderId),
                ManufacturingError.ProductionOrderNotFound);

        var operation = order.FindOperation(command.Sequence);
        if (operation is null)
            return Result<ProductionOrder>.Failure(ProductionOrderErrors.OperationNotFound(command.Sequence),
                ManufacturingError.OperationNotFound);

        operation.Update(command.Progress, command.Status, command.Reference);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<ProductionOrder>.Success(order);
    }
}
