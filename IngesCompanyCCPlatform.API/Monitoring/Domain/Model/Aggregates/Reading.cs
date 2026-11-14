using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;

/// <summary>
///     A telemetry reading of a sensor, usually sent by ThingsBoard.
/// </summary>
public partial class Reading : AggregateRoot
{
    /// <summary>
    ///     The source used when the command does not say where the reading came from.
    /// </summary>
    public const string DefaultSource = "ThingsBoard";

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected Reading()
    {
        SensorCode = null!;
        BatchCode = string.Empty;
        Source = DefaultSource;
    }

    /// <summary>
    ///     Records a reading. Without a date it takes the current UTC time; without a source, ThingsBoard.
    /// </summary>
    /// <param name="command">The data of the reading.</param>
    public Reading(CreateReadingCommand command) : this()
    {
        SensorCode = command.SensorCode;
        BatchCode = command.BatchCode?.Trim() ?? string.Empty;
        Value = command.Value;
        RecordedAt = command.RecordedAt?.ToUniversalTime() ?? DateTime.UtcNow;
        Source = string.IsNullOrWhiteSpace(command.Source) ? DefaultSource : command.Source.Trim();
    }

    /// <summary>
    ///     The id of the reading.
    /// </summary>
    public ReadingId Id { get; }

    /// <summary>
    ///     The code of the sensor that took it.
    /// </summary>
    public SensorCode SensorCode { get; private set; }

    /// <summary>
    ///     The code of the production batch being monitored, or empty.
    /// </summary>
    public string BatchCode { get; private set; }

    /// <summary>
    ///     The measured value, in the unit of the sensor.
    /// </summary>
    public decimal Value { get; private set; }

    /// <summary>
    ///     When it was measured (UTC).
    /// </summary>
    public DateTime RecordedAt { get; private set; }

    /// <summary>
    ///     Where it came from, for example ThingsBoard.
    /// </summary>
    public string Source { get; private set; }
}
