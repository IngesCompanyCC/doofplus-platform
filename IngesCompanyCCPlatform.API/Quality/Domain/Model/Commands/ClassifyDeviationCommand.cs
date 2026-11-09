using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to change the severity of a deviation.
/// </summary>
public record ClassifyDeviationCommand(DeviationId DeviationId, EDeviationSeverity Severity);
