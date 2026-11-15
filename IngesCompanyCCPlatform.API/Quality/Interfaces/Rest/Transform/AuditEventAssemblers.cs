using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>AuditEvent</c> from the resources. Enums are accepted in any case and with hyphens.
/// </summary>
public static class AuditEventCommandFromResourceAssembler
{
    /// <summary>
    ///     Builds the command to create a audit event.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateAuditEventCommand ToCommandFromResource(CreateAuditEventResource resource)
    {
        return new CreateAuditEventCommand(
            resource.EventCode ?? string.Empty,
            resource.OccurredAt,
            resource.Actor ?? string.Empty,
            resource.ActorRole ?? string.Empty,
            resource.Event ?? string.Empty,
            resource.Record ?? string.Empty,
            resource.Result ?? string.Empty,
            resource.Field ?? string.Empty,
            resource.Before ?? string.Empty,
            resource.After ?? string.Empty,
            resource.Reason ?? string.Empty);
    }
}

/// <summary>
///     Builds the resource of a audit event.
/// </summary>
public static class AuditEventResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The audit event.</param>
    /// <returns>The resource.</returns>
    public static AuditEventResource ToResourceFromEntity(AuditEvent entity)
    {
        return new AuditEventResource(
            entity.Id.Value,
            entity.EventCode,
            entity.OccurredAt is null ? null : DateTime.SpecifyKind(entity.OccurredAt.Value, DateTimeKind.Utc),
            entity.Actor,
            entity.ActorRole,
            entity.Event,
            entity.Record,
            entity.Result,
            entity.Field,
            entity.Before,
            entity.After,
            entity.Reason);
    }
}
