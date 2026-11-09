using IngesCompanyCCPlatform.API.Quality.Domain.Model.Events;
using IngesCompanyCCPlatform.API.Shared.Application.Internal.EventHandlers;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="MaterialApprovalDecidedEvent" />.
/// </summary>
/// <remarks>
///     Manufacturing will add its own handler to update the raw material lot; this one leaves the decision in the log.
/// </remarks>
public class MaterialApprovalDecidedEventHandler(ILogger<MaterialApprovalDecidedEventHandler> logger)
    : IEventHandler<MaterialApprovalDecidedEvent>
{
    /// <inheritdoc />
    public Task Handle(MaterialApprovalDecidedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Material approval decided: rawMaterialLotId={RawMaterialLotId}, approved={Approved}",
            domainEvent.RawMaterialLotId, domainEvent.Approved);
        return Task.CompletedTask;
    }
}
