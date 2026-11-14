namespace IngesCompanyCCPlatform.API.Manufacturing.Interfaces.Rest.Resources;

/// <summary>
///     Request payload to register an incident in a batch.
/// </summary>
public record RegisterIncidentResource(
    string Description,
    string Severity);
