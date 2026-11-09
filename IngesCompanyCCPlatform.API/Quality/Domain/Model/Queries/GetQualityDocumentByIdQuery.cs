using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a quality document by its id.
/// </summary>
public record GetQualityDocumentByIdQuery(QualityDocumentId QualityDocumentId);
