using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>AuditFinding</c> from the resources.
/// </summary>
public static class AuditFindingCommandFromResourceAssembler
{
    private const string SeverityInvalidMessageKey = "quality.auditFinding.error.severity.invalid";

    /// <summary>
    ///     Builds the command to create a audit finding.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateAuditFindingCommand ToCommandFromResource(CreateAuditFindingResource resource)
    {
        return new CreateAuditFindingCommand(
            resource.AuditId,
            resource.Title,
            resource.Description ?? string.Empty,
            EnumParser.Parse<EAuditFindingSeverity>(resource.Severity, SeverityInvalidMessageKey),
            resource.RelatedDeviationId);
    }
}

/// <summary>
///     Builds the resource of a audit finding.
/// </summary>
public static class AuditFindingResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The audit finding.</param>
    /// <returns>The resource.</returns>
    public static AuditFindingResource ToResourceFromEntity(AuditFinding entity)
    {
        return new AuditFindingResource(
            entity.Id.Value,
            entity.AuditId,
            entity.Title,
            entity.Description,
            entity.Severity.ToString(),
            entity.RelatedDeviationId,
            DateTime.SpecifyKind(entity.IdentifiedAt, DateTimeKind.Utc));
    }
}
