using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Request payload to register an incident in a batch.
/// </summary>
public record RegisterIncidentResource(
    string Description,
    IncidentSeverity Severity);
