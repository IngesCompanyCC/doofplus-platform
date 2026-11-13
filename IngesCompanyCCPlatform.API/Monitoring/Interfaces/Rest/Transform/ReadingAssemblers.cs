using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Monitoring.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Monitoring.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of the readings from the resources.
/// </summary>
public static class ReadingCommandFromResourceAssembler
{
    /// <summary>
    ///     Builds the command to record a reading by hand.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when the sensor code is not valid.</exception>
    public static CreateReadingCommand ToCommandFromResource(CreateReadingResource resource)
    {
        return new CreateReadingCommand(new SensorCode(resource.SensorCode), resource.BatchCode ?? string.Empty,
            resource.Value, resource.RecordedAt, resource.Source ?? string.Empty);
    }

    /// <summary>
    ///     Builds the command with a reading sent by ThingsBoard.
    /// </summary>
    /// <param name="resource">The payload sent by ThingsBoard.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when the device id is not valid.</exception>
    public static RecordTelemetryCommand ToCommandFromResource(TelemetryResource resource)
    {
        return new RecordTelemetryCommand(new DeviceId(resource.DeviceId), resource.Value, resource.RecordedAt);
    }
}

/// <summary>
///     Builds the resource of a reading.
/// </summary>
public static class ReadingResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The reading.</param>
    /// <returns>The resource.</returns>
    public static ReadingResource ToResourceFromEntity(Reading entity)
    {
        return new ReadingResource(entity.Id.Value, entity.SensorCode.Value, entity.BatchCode, entity.Value,
            DateTime.SpecifyKind(entity.RecordedAt, DateTimeKind.Utc), entity.Source);
    }
}
