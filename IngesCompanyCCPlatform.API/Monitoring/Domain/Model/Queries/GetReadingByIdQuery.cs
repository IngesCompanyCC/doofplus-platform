using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Queries;

/// <summary>
///     Query to get a reading by its id.
/// </summary>
public record GetReadingByIdQuery(ReadingId ReadingId);
