using IngesCompanyCCPlatform.API.Quality.Domain.Model.Events;
using IngesCompanyCCPlatform.API.Shared.Application.Internal.EventHandlers;

namespace IngesCompanyCCPlatform.API.Quality.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="DeviationClosedEvent" />.
/// </summary>
/// <remarks>
///     Policy "when a deviation is closed, evaluate the affected batch again": the batch review is
///     evaluated again with <c>POST /api/v1/batch-reviews/{id}/evaluation</c>; this handler records the closing.
/// </remarks>
public class DeviationClosedEventHandler(ILogger<DeviationClosedEventHandler> logger)
    : IEventHandler<DeviationClosedEvent>
{
    /// <inheritdoc />
    public Task Handle(DeviationClosedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deviation closed: code={Code}, productionBatchId={ProductionBatchId}",
            domainEvent.Code, domainEvent.ProductionBatchId);
        return Task.CompletedTask;
    }
}
