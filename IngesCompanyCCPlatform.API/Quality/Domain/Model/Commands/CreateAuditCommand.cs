using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a audit.
/// </summary>
public record CreateAuditCommand(
    string Code,
    string Title,
    string Period,
    int AreasReviewed,
    int AreasTotal,
    EAuditStatus Status);
