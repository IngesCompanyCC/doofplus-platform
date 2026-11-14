namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a quality document by its id.
/// </summary>
/// <param name="QualityDocumentId">The id of the quality document.</param>
public record GetQualityDocumentByIdQuery(int QualityDocumentId);
