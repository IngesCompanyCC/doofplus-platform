using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to add a new revision to a quality document.
/// </summary>
public record PublishDocumentVersionCommand(QualityDocumentId QualityDocumentId, string FileUrl, string ChangeReason, UserId AuthorId);
