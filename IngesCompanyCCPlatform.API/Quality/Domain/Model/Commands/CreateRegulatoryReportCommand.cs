using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a regulatory report.
/// </summary>
public record CreateRegulatoryReportCommand(
    string Code,
    string Title,
    string Description,
    string RegulatoryAuthority,
    string FileReference,
    DateOnly? DueDate,
    int? PreparedBy);
