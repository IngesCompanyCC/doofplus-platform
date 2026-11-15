namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data that replaces a quality document.
/// </summary>
public record UpdateQualityDocumentResource(
    string? Code,
    string? Version,
    string? Title,
    string? Status,
    string? Author,
    string? TechnicalReviewer,
    string? Approver,
    DateOnly? ChangedAt,
    string? Purpose,
    string? Scope,
    string? Procedure,
    string? RevisionSummary,
    int? ReviewCycleMonths);
