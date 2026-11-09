using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to add a corrective or preventive action to a deviation.
/// </summary>
public record AddCapaActionCommand(DeviationId DeviationId, ECapaType Type, string Description, UserId ResponsibleId, DateOnly DueDate);
