namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A analytical result, as the API returns it.
/// </summary>
public record AnalyticalResultResource(
    int Id,
    int BatchId,
    string TestName,
    string Parameter,
    decimal MeasuredValue,
    decimal MinimumValue,
    decimal MaximumValue,
    string Unit,
    string Status,
    int? ReviewedBy,
    DateTime? TestedAt,
    DateTime? ReviewedAt);
