using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;

/// <summary>
///     Command to replace the data of a analytical result.
/// </summary>
public record UpdateAnalyticalResultCommand(
    int AnalyticalResultId,
    string BatchCode,
    string SampleCode,
    string ReportCode,
    string Test,
    string Protocol,
    string Result,
    string Specification,
    string Analyst,
    EResultStatus Status,
    EApprovalState Approval);
