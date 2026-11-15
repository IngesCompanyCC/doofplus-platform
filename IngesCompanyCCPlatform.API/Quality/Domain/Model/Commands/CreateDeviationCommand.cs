using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to create a deviation.
/// </summary>
public record CreateDeviationCommand(
    string Code,
    string Title,
    string BatchCode,
    string OrderCode,
    string Summary,
    EClassification Classification,
    string Containment,
    int Severity,
    int Likelihood,
    int Detectability,
    EDeviationStatus Status,
    string RootCause,
    string CapaCode,
    string ReportedBy,
    DateTime? ReportedAt,
    string Owner,
    DateTime? DueAt);
