using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a audit finding.
/// </summary>
public record CreateAuditFindingCommand(
    string AuditCode,
    string Code,
    string Title,
    EClassification Classification,
    string Owner,
    DateOnly? DueAt,
    EFindingStatus Status,
    string Observation,
    string Response,
    string ClosureRequirement);
