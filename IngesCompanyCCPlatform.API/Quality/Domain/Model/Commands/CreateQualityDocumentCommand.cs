using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a quality document.
/// </summary>
public record CreateQualityDocumentCommand(
    string Code,
    string Version,
    string Title,
    EDocumentStatus Status,
    string Author,
    string TechnicalReviewer,
    string Approver,
    DateOnly? ChangedAt,
    string Purpose,
    string Scope,
    string Procedure,
    string RevisionSummary,
    int ReviewCycleMonths);
