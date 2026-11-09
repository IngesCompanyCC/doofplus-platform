using IngesCompanyCCPlatform.API.Quality.Domain.Model.Events;
using IngesCompanyCCPlatform.API.Shared.Application.Internal.EventHandlers;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="CriticalDeviationRegisteredEvent" />.
/// </summary>
/// <remarks>
///     The notification by email (SendGrid) is not integrated yet; for now the alert is left in the log.
/// </remarks>
public class CriticalDeviationRegisteredEventHandler(ILogger<CriticalDeviationRegisteredEventHandler> logger)
    : IEventHandler<CriticalDeviationRegisteredEvent>
{
    /// <inheritdoc />
    public Task Handle(CriticalDeviationRegisteredEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogWarning("Critical deviation registered: code={Code}, productionBatchId={ProductionBatchId}",
            domainEvent.Code, domainEvent.ProductionBatchId);
        return Task.CompletedTask;
    }
}
