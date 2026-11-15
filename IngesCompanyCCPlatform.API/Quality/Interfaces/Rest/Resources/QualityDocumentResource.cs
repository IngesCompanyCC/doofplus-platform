namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

/// <summary>
///     A quality document, with the fields of <c>contratos/quality-api.json</c>.
/// </summary>
public record QualityDocumentResource(
    int Id,
    string Code,
    string Version,
    string Title,
    string Status,
    string Author,
    string TechnicalReviewer,
    string Approver,
    DateOnly? ChangedAt,
    string Purpose,
    string Scope,
    string Procedure,
    string RevisionSummary,
    int ReviewCycleMonths);
