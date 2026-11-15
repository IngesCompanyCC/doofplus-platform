using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>Audit</c> from the resources. Enums are accepted in any case and with hyphens.
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
            resource.Code ?? string.Empty,
            resource.Title ?? string.Empty,
            resource.Period ?? string.Empty,
            resource.AreasReviewed ?? 0,
            resource.AreasTotal ?? 0,
            resource.Status is null ? EAuditStatus.Scheduled : EnumParser.Parse<EAuditStatus>(resource.Status, "quality.audit.error.status.invalid"));
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
            entity.Period,
            entity.AreasReviewed,
            entity.AreasTotal,
            entity.Status.ToString());
    }
}
