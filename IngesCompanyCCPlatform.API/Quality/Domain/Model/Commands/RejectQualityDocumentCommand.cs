using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to send a quality document under review back to draft.
/// </summary>
public record RejectQualityDocumentCommand(QualityDocumentId QualityDocumentId, string Reason);
