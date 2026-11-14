using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a deviation.
/// </summary>
public record CreateDeviationCommand(
    string Code,
    string Title,
    string Description,
    EDeviationSeverity Severity,
    int BatchId,
    int? AssignedTo);
