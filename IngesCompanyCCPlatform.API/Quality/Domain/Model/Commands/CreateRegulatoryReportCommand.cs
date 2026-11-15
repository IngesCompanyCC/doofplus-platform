using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a regulatory report.
/// </summary>
public record CreateRegulatoryReportCommand(
    string Code,
    EReportType Type,
    string Scope,
    string Owner,
    EReportStatus Status,
    string Template,
    string Format,
    DateOnly? CreatedOn);
