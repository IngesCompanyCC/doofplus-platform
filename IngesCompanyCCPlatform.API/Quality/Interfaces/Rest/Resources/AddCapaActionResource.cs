namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to add a CAPA action to a deviation. Type: Corrective or Preventive.
/// </summary>
public record AddCapaActionResource(string Type, string Description, int ResponsibleId, DateOnly DueDate);
