using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to start the investigation of an open deviation.
/// </summary>
public record StartDeviationInvestigationCommand(DeviationId DeviationId);
