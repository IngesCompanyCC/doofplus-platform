namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query of the quality documents, optionally filtered by code.
/// </summary>
public record GetAllQualityDocumentsQuery(string? Code = null);
