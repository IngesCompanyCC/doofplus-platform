using IngesCompanyCCPlatform.API.Quality.Domain.Model.Events;
using IngesCompanyCCPlatform.API.Shared.Application.Internal.EventHandlers;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="BatchReleaseDecidedEvent" />.
/// </summary>
/// <remarks>
///     Manufacturing will add its own handler to update the production batch; this one leaves the verdict in the log.
/// </remarks>
public class BatchReleaseDecidedEventHandler(ILogger<BatchReleaseDecidedEventHandler> logger)
    : IEventHandler<BatchReleaseDecidedEvent>
{
    /// <inheritdoc />
    public Task Handle(BatchReleaseDecidedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Batch release decided: productionBatchId={ProductionBatchId}, verdict={Verdict}, certificate={CertificateNumber}",
            domainEvent.ProductionBatchId, domainEvent.Verdict, domainEvent.CertificateNumber);
        return Task.CompletedTask;
    }
}
