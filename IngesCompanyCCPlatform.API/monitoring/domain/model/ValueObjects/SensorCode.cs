using System.Text.RegularExpressions;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;

public partial record SensorCode{
    private const int MaxLength = 64;
    private const string NotBlankMessageKey = "monitoring.sensor.error.code.notBlank";
    private const string SizeMessageKey = "monitoring.sensor.error.code.size";
    private const string PatternMessageKey = "monitoring.sensor.error.code.pattern";

    public string Value { get; }

    public SensorCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException(NotBlankMessageKey); }

        if (value.Length > MaxLength) { throw new ArgumentException(SizeMessageKey); }

        if (!AllowedPattern().IsMatch(value)) { throw new ArgumentException(PatternMessageKey); }

        Value = value;
    }

    [GeneratedRegex("^[a-z0-9]+(-[a-z0-9]+)*$")]

    private static partial Regex AllowedPattern();

}