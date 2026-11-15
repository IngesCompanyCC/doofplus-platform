namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query of the evidence, optionally filtered by record code.
/// </summary>
public record GetAllEvidenceQuery(string? RecordCode = null);
