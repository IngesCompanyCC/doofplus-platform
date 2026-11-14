using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

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
            entity.AggregateType,
            entity.AggregateId,
            entity.EventType,
            entity.Description,
            entity.PerformedBy,
            DateTime.SpecifyKind(entity.OccurredAt, DateTimeKind.Utc));
    }
}
