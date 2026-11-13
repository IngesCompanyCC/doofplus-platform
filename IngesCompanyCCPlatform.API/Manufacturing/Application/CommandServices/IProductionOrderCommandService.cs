using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;

/// <summary>
///     Command service of the production orders.
/// </summary>
public interface IProductionOrderCommandService
{
    /// <summary>
    ///     Plans an order that follows an approved formula.
    /// </summary>
    Task<Result<ProductionOrder>> Handle(CreateProductionOrderCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Starts, holds, resumes or completes an order.
    /// </summary>
    Task<Result<ProductionOrder>> Handle(ChangeProductionOrderStatusCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Records the progress of an operation of an order.
    /// </summary>
    Task<Result<ProductionOrder>> Handle(UpdateOperationCommand command, CancellationToken cancellationToken);
}
