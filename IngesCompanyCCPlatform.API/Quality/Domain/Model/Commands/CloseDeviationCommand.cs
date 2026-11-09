using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to close a deviation once its root cause and CAPA actions are verified.
/// </summary>
public record CloseDeviationCommand(DeviationId DeviationId);
