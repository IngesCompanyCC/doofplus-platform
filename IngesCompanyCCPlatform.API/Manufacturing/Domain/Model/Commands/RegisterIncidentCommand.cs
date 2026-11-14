using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Commands;

/// <summary>
///     Command to register an incident occurred during batch production.
/// </summary>
public record RegisterIncidentCommand(
    int BatchId,
    string Description,
    IncidentSeverity Severity);
