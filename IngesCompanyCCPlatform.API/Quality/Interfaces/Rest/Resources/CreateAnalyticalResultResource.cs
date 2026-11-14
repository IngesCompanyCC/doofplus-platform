namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a analytical result.
/// </summary>
public record CreateAnalyticalResultResource(
    int BatchId,
    string TestName,
    string? Parameter,
    decimal MeasuredValue,
    decimal MinimumValue,
    decimal MaximumValue,
    string? Unit,
    DateTime? TestedAt);
