namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to update the progress of a CAPA action, from 0 to 100.
/// </summary>
public record UpdateCapaActionProgressResource(int Progress);
