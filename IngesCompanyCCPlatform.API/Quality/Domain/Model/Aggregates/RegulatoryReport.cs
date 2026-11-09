using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;

/// <summary>
///     Represents the RegulatoryReport aggregate root within the Quality &amp; Compliance bounded context.
/// </summary>
/// <remarks>
///     Keeps the monthly report generated for an organization, ready to be presented to DIGEMID.
/// </remarks>
public class RegulatoryReport : AggregateRoot
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RegulatoryReport" /> aggregate. Used by Entity Framework Core.
    /// </summary>
    protected RegulatoryReport()
    {
        Period = null!;
        FileUrl = string.Empty;
    }

    /// <summary>
    ///     Initializes a new regulatory report.
    /// </summary>
    /// <param name="command">The <see cref="RegisterRegulatoryReportCommand" /> with the report data.</param>
    public RegulatoryReport(RegisterRegulatoryReportCommand command) : this()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command.FileUrl);
        OrganizationId = command.OrganizationId;
        Period = command.Period;
        FileUrl = command.FileUrl;
        GeneratedAt = DateTime.UtcNow;
    }

    public RegulatoryReportId Id { get; }
    public OrganizationId OrganizationId { get; private set; }
    public ReportPeriod Period { get; private set; }
    public string FileUrl { get; private set; }
    public DateTime GeneratedAt { get; private set; }
}
