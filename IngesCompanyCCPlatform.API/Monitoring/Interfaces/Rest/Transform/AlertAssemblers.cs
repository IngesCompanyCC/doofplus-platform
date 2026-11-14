using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of the alerts from the resources.
/// </summary>
public static class AlertCommandFromResourceAssembler
{
    private const string SeverityInvalidMessageKey = "monitoring.alert.error.severity.invalid";
    private const string StatusInvalidMessageKey = "monitoring.alert.error.status.invalid";

    /// <summary>
    ///     Builds the command to raise an alert by hand.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when the code or the severity is not valid.</exception>
    public static CreateAlertCommand ToCommandFromResource(CreateAlertResource resource)
    {
        return new CreateAlertCommand(
            new AlertCode(resource.Code),
            resource.SensorCode ?? string.Empty,
            resource.EquipmentCode ?? string.Empty,
            EnumParser.Parse<EAlertSeverity>(resource.Severity, SeverityInvalidMessageKey),
            resource.Title,
            resource.Detail ?? string.Empty,
            resource.BatchCode ?? string.Empty,
            resource.LinkedRecord ?? string.Empty);
    }

    /// <summary>
    ///     Builds the command with the handling of an alert.
    /// </summary>
    /// <param name="alertId">The id from the route.</param>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when the status is not valid.</exception>
    public static UpdateAlertCommand ToCommandFromResource(int alertId, UpdateAlertResource resource)
    {
        return new UpdateAlertCommand(
            alertId,
            EnumParser.Parse<EAlertStatus>(resource.Status, StatusInvalidMessageKey),
            resource.AcknowledgedBy ?? string.Empty,
            resource.ResponseNote ?? string.Empty,
            resource.LinkedRecord ?? string.Empty);
    }
}

/// <summary>
///     Builds the resource of an alert.
/// </summary>
public static class AlertResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The alert.</param>
    /// <returns>The resource.</returns>
    public static AlertResource ToResourceFromEntity(Alert entity)
    {
        return new AlertResource(entity.Id.Value, entity.Code.Value, entity.SensorCode, entity.EquipmentCode,
            entity.Severity.ToString(), entity.Status.ToString(), entity.Title, entity.Detail, entity.BatchCode,
            entity.AcknowledgedBy,
            entity.AcknowledgedAt is null ? null : DateTime.SpecifyKind(entity.AcknowledgedAt.Value, DateTimeKind.Utc),
            entity.ResponseNote, entity.LinkedRecord);
    }
}
