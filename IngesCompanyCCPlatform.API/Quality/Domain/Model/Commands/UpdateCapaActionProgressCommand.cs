using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to update the progress of a CAPA action.
/// </summary>
public record UpdateCapaActionProgressCommand(DeviationId DeviationId, int CapaActionId, int Progress);
