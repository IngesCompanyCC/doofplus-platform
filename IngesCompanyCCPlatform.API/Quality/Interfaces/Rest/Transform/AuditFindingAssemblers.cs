using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>AuditFinding</c> from the resources. Enums are accepted in any case and with hyphens.
/// </summary>
public static class AuditFindingCommandFromResourceAssembler
{
    /// <summary>
    ///     Builds the command to create a audit finding.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateAuditFindingCommand ToCommandFromResource(CreateAuditFindingResource resource)
    {
        return new CreateAuditFindingCommand(
            resource.AuditCode ?? string.Empty,
            resource.Code ?? string.Empty,
            resource.Title ?? string.Empty,
            resource.Classification is null ? EClassification.Minor : EnumParser.Parse<EClassification>(resource.Classification, "quality.auditFinding.error.classification.invalid"),
            resource.Owner ?? string.Empty,
            resource.DueAt,
            resource.Status is null ? EFindingStatus.EvidencePending : EnumParser.Parse<EFindingStatus>(resource.Status, "quality.auditFinding.error.status.invalid"),
            resource.Observation ?? string.Empty,
            resource.Response ?? string.Empty,
            resource.ClosureRequirement ?? string.Empty);
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
            entity.AuditCode,
            entity.Code,
            entity.Title,
            entity.Classification.ToString(),
            entity.Owner,
            entity.DueAt,
            entity.Status.ToString(),
            entity.Observation,
            entity.Response,
            entity.ClosureRequirement);
    }
}
