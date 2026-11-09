using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Events;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Events;

/// <summary>
///     Raised when a critical deviation is registered, so the quality team is notified.
/// </summary>
public record CriticalDeviationRegisteredEvent(string Code, ProductionBatchId ProductionBatchId) : IEvent;
