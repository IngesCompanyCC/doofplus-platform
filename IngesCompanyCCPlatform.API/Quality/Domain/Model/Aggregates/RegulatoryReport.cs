using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     A report for a regulatory authority, for example DIGEMID.
/// </summary>
public partial class RegulatoryReport : AggregateRoot
{
    private const string CodeNotBlankMessageKey = "quality.regulatoryReport.error.code.notBlank";
    private const string TitleNotBlankMessageKey = "quality.regulatoryReport.error.title.notBlank";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected RegulatoryReport()
    {
        Code = string.Empty;
        Title = string.Empty;
        Description = string.Empty;
        RegulatoryAuthority = string.Empty;
        FileReference = string.Empty;
    }

    /// <summary>
    ///     Creates the regulatory report from its command.
    /// </summary>
    /// <param name="command">The data of the regulatory report.</param>
    /// <exception cref="ArgumentException">Thrown when a required value is blank.</exception>
    public RegulatoryReport(CreateRegulatoryReportCommand command) : this()
    {
        if (string.IsNullOrWhiteSpace(command.Code)) throw new ArgumentException(CodeNotBlankMessageKey);
        Code = command.Code.Trim();
        if (string.IsNullOrWhiteSpace(command.Title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Title = command.Title.Trim();
        Description = command.Description?.Trim() ?? string.Empty;
        RegulatoryAuthority = command.RegulatoryAuthority?.Trim() ?? string.Empty;
        FileReference = command.FileReference?.Trim() ?? string.Empty;
        DueDate = command.DueDate;
        PreparedBy = command.PreparedBy;
        Status = ERegulatoryReportStatus.Draft;
    }

    /// <summary>
    ///     The id of the regulatory report.
    /// </summary>
    public RegulatoryReportId Id { get; }

    /// <summary>
    ///     The business code, for example RR-2026-04.
    /// </summary>
    public string Code { get; private set; }
    /// <summary>
    ///     The title of the report.
    /// </summary>
    public string Title { get; private set; }
    /// <summary>
    ///     What the report contains.
    /// </summary>
    public string Description { get; private set; }
    /// <summary>
    ///     The authority that receives the report.
    /// </summary>
    public string RegulatoryAuthority { get; private set; }
    /// <summary>
    ///     Where the file of the report is.
    /// </summary>
    public string FileReference { get; private set; }
    /// <summary>
    ///     When the report has to be submitted.
    /// </summary>
    public DateOnly? DueDate { get; private set; }
    /// <summary>
    ///     The id of the user who prepared it.
    /// </summary>
    public int? PreparedBy { get; private set; }
    /// <summary>
    ///     Where the report is.
    /// </summary>
    public ERegulatoryReportStatus Status { get; private set; }
    /// <summary>
    ///     When the report was submitted (UTC).
    /// </summary>
    public DateTime? SubmittedAt { get; private set; }

    /// <summary>
    ///     Sends a draft to review.
    /// </summary>
    public bool SubmitForReview()
    {
        if (Status != ERegulatoryReportStatus.Draft) return false;
        Status = ERegulatoryReportStatus.UnderReview;
        return true;
    }

    /// <summary>
    ///     Approves a report under review.
    /// </summary>
    public bool Approve()
    {
        if (Status != ERegulatoryReportStatus.UnderReview) return false;
        Status = ERegulatoryReportStatus.Approved;
        return true;
    }

    /// <summary>
    ///     Submits an approved report to the authority.
    /// </summary>
    public bool Submit()
    {
        if (Status != ERegulatoryReportStatus.Approved) return false;
        Status = ERegulatoryReportStatus.Submitted;
        SubmittedAt = DateTime.UtcNow;
        return true;
    }

    /// <summary>
    ///     Rejects a report under review.
    /// </summary>
    public bool Reject()
    {
        if (Status != ERegulatoryReportStatus.UnderReview) return false;
        Status = ERegulatoryReportStatus.Rejected;
        return true;
    }
}
