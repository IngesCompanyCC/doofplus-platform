using System.Globalization;

namespace IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;

/// <summary>
///     Represents the month covered by a regulatory report, written as <c>YYYY-MM</c>.
/// </summary>
public record ReportPeriod
{
    private const string Format = "yyyy-MM";
    private const string PatternMessageKey = "regulatoryReport.error.period.pattern";

    /// <param name="value">The period, for example <c>2026-10</c>.</param>
    public ReportPeriod(string value)
    {
        if (!DateTime.TryParseExact(value, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            throw new ArgumentException(PatternMessageKey);
        Value = value;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
