using IngesCompanyCCPlatform.API.Shared.Domain.Model.Events;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Events;

/// <summary>
///     Published when an incident is registered on a production batch, so other bounded contexts can react to it.
/// </summary>
/// <remarks>
///     It only carries plain values, so its handlers do not depend on the Manufacturing model.
/// </remarks>
/// <param name="ProductionBatchId">The id of the batch.</param>
/// <param name="BatchNumber">The batch number, for example B-26041.</param>
/// <param name="Line">The production line or equipment where it happened, if the batch has one.</param>
/// <param name="Description">What happened.</param>
/// <param name="Severity">The severity: Minor, Major or Critical.</param>
/// <param name="RecordedAt">When the incident was registered (UTC).</param>
public record IncidentRegisteredEvent(
    int ProductionBatchId,
    string BatchNumber,
    string? Line,
    string Description,
    string Severity,
    DateTime RecordedAt) : IEvent;
