using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a analytical result.
/// </summary>
public record CreateAnalyticalResultCommand(
    int BatchId,
    string TestName,
    string Parameter,
    decimal MeasuredValue,
    decimal MinimumValue,
    decimal MaximumValue,
    string Unit,
    DateTime? TestedAt);
