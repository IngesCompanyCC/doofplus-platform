namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Response resource representing a CAPA action.
/// </summary>
public record CapaActionResource(int Id, string Type, string Description, int ResponsibleId, DateOnly DueDate, int Progress, string Status, bool IsOverdue);
