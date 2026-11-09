using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Queries;

/// <summary>
///     Query to get an audit by its id.
/// </summary>
public record GetAuditByIdQuery(AuditId AuditId);
