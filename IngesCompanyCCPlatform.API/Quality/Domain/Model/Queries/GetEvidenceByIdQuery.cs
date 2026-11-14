namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get a evidence by its id.
/// </summary>
/// <param name="EvidenceId">The id of the evidence.</param>
public record GetEvidenceByIdQuery(int EvidenceId);
