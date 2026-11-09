using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a controlled quality document with its first version.
/// </summary>
public record CreateQualityDocumentCommand(string Code, string Title, EDocumentType Type, string FileUrl, UserId AuthorId);
