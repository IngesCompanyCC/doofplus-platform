using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a quality document.
/// </summary>
public record CreateQualityDocumentCommand(
    string Code,
    string Title,
    string Description,
    string DocumentType,
    string FileReference,
    string Version,
    int? OwnerId);
