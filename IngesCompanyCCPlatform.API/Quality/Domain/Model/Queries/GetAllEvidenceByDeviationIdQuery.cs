namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get the evidence filtered by deviation id.
/// </summary>
public record GetAllEvidenceByDeviationIdQuery(int DeviationId);
