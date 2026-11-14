using System.Text.RegularExpressions;

namespace IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a unique production batch identifier (e.g., "B-26041" or alphanumeric code).
/// </summary>
public record BatchNumber
{
    private static readonly Regex Pattern = new(@"^[A-Za-z0-9-]+$", RegexOptions.Compiled);
    private const int MaxLength = 30;

    public string Value { get; }

    public BatchNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Batch number cannot be empty.", nameof(value));

        var cleaned = value.Trim();

        if (cleaned.Length > MaxLength)
            throw new ArgumentException($"Batch number cannot exceed {MaxLength} characters.", nameof(value));

        if (!Pattern.IsMatch(cleaned))
            throw new ArgumentException("Batch number contains invalid characters.", nameof(value));

        Value = cleaned;
    }

    public override string ToString() => Value;
}
