using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to register the root cause of a deviation under investigation.
/// </summary>
public record RegisterRootCauseCommand(DeviationId DeviationId, RootCause RootCause);
