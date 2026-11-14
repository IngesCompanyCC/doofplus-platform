using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>Audit</c> from the resources.
/// </summary>
public static class AuditCommandFromResourceAssembler
{


    /// <summary>
    ///     Builds the command to create a audit.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateAuditCommand ToCommandFromResource(CreateAuditResource resource)
    {
        return new CreateAuditCommand(
            resource.Code,
            resource.Title,
            resource.Scope ?? string.Empty,
            resource.AuditType ?? string.Empty,
            resource.AuditorId,
            resource.PlannedDate);
    }
}

/// <summary>
///     Builds the resource of a audit.
/// </summary>
public static class AuditResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The audit.</param>
    /// <returns>The resource.</returns>
    public static AuditResource ToResourceFromEntity(Audit entity)
    {
        return new AuditResource(
            entity.Id.Value,
            entity.Code,
            entity.Title,
            entity.Scope,
            entity.AuditType,
            entity.AuditorId,
            entity.PlannedDate,
            entity.Status.ToString(),
            entity.StartedAt is null ? null : DateTime.SpecifyKind(entity.StartedAt.Value, DateTimeKind.Utc),
            entity.CompletedAt is null ? null : DateTime.SpecifyKind(entity.CompletedAt.Value, DateTimeKind.Utc));
    }
}
