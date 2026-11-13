using System.Globalization;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;

/// <summary>
///     An alert raised when a sensor reads a value outside its accepted range, or raised by hand.
/// </summary>
public partial class Alert : AggregateRoot
{
    private const string TitleNotBlankMessageKey = "monitoring.alert.error.title.notBlank";

    /// <summary>
    ///     Share of the accepted range beyond a limit from which an alert is critical instead of a warning.
    /// </summary>
    private const decimal CriticalShareOfRange = 0.10m;

    /// <summary>
    ///     Constructor used by Entity Framework Core.
    /// </summary>
    protected Alert()
    {
        Code = null!;
        SensorCode = string.Empty;
        EquipmentCode = string.Empty;
        Title = string.Empty;
        Detail = string.Empty;
        BatchCode = string.Empty;
        AcknowledgedBy = string.Empty;
        ResponseNote = string.Empty;
        LinkedRecord = string.Empty;
    }

    /// <summary>
    ///     Raises an alert by hand. It starts as <see cref="EAlertStatus.Open" />.
    /// </summary>
    /// <param name="command">The data of the alert.</param>
    /// <exception cref="ArgumentException">Thrown when the title is blank.</exception>
    public Alert(CreateAlertCommand command) : this()
    {
        if (string.IsNullOrWhiteSpace(command.Title)) throw new ArgumentException(TitleNotBlankMessageKey);
        Code = command.Code;
        SensorCode = command.SensorCode?.Trim() ?? string.Empty;
        EquipmentCode = command.EquipmentCode?.Trim() ?? string.Empty;
        Severity = command.Severity;
        Status = EAlertStatus.Open;
        Title = command.Title.Trim();
        Detail = command.Detail?.Trim() ?? string.Empty;
        BatchCode = command.BatchCode?.Trim() ?? string.Empty;
        LinkedRecord = command.LinkedRecord?.Trim() ?? string.Empty;
    }

    /// <summary>
    ///     The id of the alert.
    /// </summary>
    public AlertId Id { get; }

    /// <summary>
    ///     The business code, for example ALT-26031.
    /// </summary>
    public AlertCode Code { get; private set; }

    /// <summary>
    ///     The code of the sensor that raised it.
    /// </summary>
    public string SensorCode { get; private set; }

    /// <summary>
    ///     The code of the equipment where the sensor is installed.
    /// </summary>
    public string EquipmentCode { get; private set; }

    /// <summary>
    ///     How serious the alert is.
    /// </summary>
    public EAlertSeverity Severity { get; private set; }

    /// <summary>
    ///     Where the alert is in its handling.
    /// </summary>
    public EAlertStatus Status { get; private set; }

    /// <summary>
    ///     A short title, for example Temperature excursion.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    ///     What happened, with the value and the limits.
    /// </summary>
    public string Detail { get; private set; }

    /// <summary>
    ///     The code of the affected production batch, or empty.
    /// </summary>
    public string BatchCode { get; private set; }

    /// <summary>
    ///     Who took charge of the alert.
    /// </summary>
    public string AcknowledgedBy { get; private set; }

    /// <summary>
    ///     When someone took charge of the alert (UTC).
    /// </summary>
    public DateTime? AcknowledgedAt { get; private set; }

    /// <summary>
    ///     What was done in response.
    /// </summary>
    public string ResponseNote { get; private set; }

    /// <summary>
    ///     The code of the related record, for example a deviation (DEV-26017).
    /// </summary>
    public string LinkedRecord { get; private set; }

    /// <summary>
    ///     Raises the alert of a reading outside the accepted range of its sensor.
    /// </summary>
    /// <remarks>
    ///     The alert is critical when the value goes beyond a limit by more than 10 % of the accepted range,
    ///     and a warning otherwise.
    /// </remarks>
    /// <param name="sensor">The sensor that took the reading.</param>
    /// <param name="reading">The reading outside the range.</param>
    /// <returns>The new open alert.</returns>
    public static Alert FromOutOfRangeReading(Sensor sensor, Reading reading)
    {
        var range = sensor.UpperLimit - sensor.LowerLimit;
        var excess = reading.Value > sensor.UpperLimit
            ? reading.Value - sensor.UpperLimit
            : sensor.LowerLimit - reading.Value;
        var severity = excess > range * CriticalShareOfRange ? EAlertSeverity.Critical : EAlertSeverity.Warning;
        var direction = reading.Value > sensor.UpperLimit ? "above" : "below";
        var value = reading.Value.ToString("0.##", CultureInfo.InvariantCulture);
        var lower = sensor.LowerLimit.ToString("0.##", CultureInfo.InvariantCulture);
        var upper = sensor.UpperLimit.ToString("0.##", CultureInfo.InvariantCulture);
        var code = new AlertCode($"ALT-{reading.RecordedAt:yyMMddHHmmss}-{sensor.Code.Value}");
        return new Alert(new CreateAlertCommand(
            code,
            sensor.Code.Value,
            sensor.EquipmentCode.Value,
            severity,
            $"{sensor.Type} {direction} the limit",
            $"{sensor.Code.Value} read {value} {sensor.Unit} at {reading.RecordedAt:HH:mm} UTC; accepted range {lower}–{upper} {sensor.Unit}.",
            reading.BatchCode,
            string.Empty));
    }

    /// <summary>
    ///     Records the handling of the alert. When it is acknowledged for the first time, the time is recorded.
    /// </summary>
    /// <param name="command">The new status, who took charge, the response and the linked record.</param>
    public void Update(UpdateAlertCommand command)
    {
        Status = command.Status;
        AcknowledgedBy = command.AcknowledgedBy?.Trim() ?? string.Empty;
        ResponseNote = command.ResponseNote?.Trim() ?? string.Empty;
        LinkedRecord = command.LinkedRecord?.Trim() ?? string.Empty;
        if (Status != EAlertStatus.Open && AcknowledgedAt is null) AcknowledgedAt = DateTime.UtcNow;
    }
}
