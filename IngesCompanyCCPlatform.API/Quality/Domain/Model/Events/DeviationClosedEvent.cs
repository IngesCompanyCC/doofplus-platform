using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Events;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Events;

/// <summary>
///     Raised when a deviation is closed, so the affected batch is evaluated again.
/// </summary>
public record DeviationClosedEvent(string Code, ProductionBatchId ProductionBatchId) : IEvent;
