namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     Data to create a quality document. Missing optional values take their default.
/// </summary>
public record CreateQualityDocumentResource(
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
