using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to verify the effectiveness of an implemented CAPA action.
/// </summary>
public record VerifyCapaActionCommand(DeviationId DeviationId, int CapaActionId);
