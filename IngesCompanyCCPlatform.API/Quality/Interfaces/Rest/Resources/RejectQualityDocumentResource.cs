namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Request resource to send a quality document in review back to draft.
/// </summary>
public record RejectQualityDocumentResource(string Reason);
