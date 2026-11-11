using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Manufacturing.Application.CommandServices;

/// <summary>
///     Command service contract for ProductionBatch operations in the Manufacturing bounded context.
/// </summary>
public interface IProductionBatchCommandService
{
    /// <summary>
    ///     Handles the creation of a new production batch.
    /// </summary>
    /// <param name="command">The create batch command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result containing the created batch or a domain error.</returns>
    Task<Result<ProductionBatch>> Handle(CreateBatchCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles closing a production batch with manufactured quantity.
    /// </summary>
    /// <param name="command">The close batch command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result containing the closed batch or a domain error.</returns>
    Task<Result<ProductionBatch>> Handle(CloseBatchCommand command, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Handles registering an incident in a production batch.
    /// </summary>
    /// <param name="command">The register incident command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result containing the registered incident or a domain error.</returns>
    Task<Result<Incident>> Handle(RegisterIncidentCommand command, CancellationToken cancellationToken = default);
}
