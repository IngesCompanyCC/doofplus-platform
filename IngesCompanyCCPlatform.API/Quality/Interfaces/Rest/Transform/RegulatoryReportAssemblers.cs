using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>RegulatoryReport</c> from the resources. Enums are accepted in any case and with hyphens.
/// </summary>
public static class RegulatoryReportCommandFromResourceAssembler
{
    /// <summary>
    ///     Builds the command to create a regulatory report.
    /// </summary>
    /// <param name="resource">The data sent by the client.</param>
    /// <returns>The command.</returns>
    /// <exception cref="ArgumentException">Thrown when an enum value is not valid.</exception>
    public static CreateRegulatoryReportCommand ToCommandFromResource(CreateRegulatoryReportResource resource)
    {
        return new CreateRegulatoryReportCommand(
            resource.Code ?? string.Empty,
            resource.Type is null ? EReportType.QualityReview : EnumParser.Parse<EReportType>(resource.Type, "quality.regulatoryReport.error.type.invalid"),
            resource.Scope ?? string.Empty,
            resource.Owner ?? string.Empty,
            resource.Status is null ? EReportStatus.Draft : EnumParser.Parse<EReportStatus>(resource.Status, "quality.regulatoryReport.error.status.invalid"),
            resource.Template ?? string.Empty,
            resource.Format ?? string.Empty,
            resource.CreatedAt);
    }
}

/// <summary>
///     Builds the resource of a regulatory report.
/// </summary>
public static class RegulatoryReportResourceFromEntityAssembler
{
    /// <summary>
    ///     Builds the resource from the aggregate.
    /// </summary>
    /// <param name="entity">The regulatory report.</param>
    /// <returns>The resource.</returns>
    public static RegulatoryReportResource ToResourceFromEntity(RegulatoryReport entity)
    {
        return new RegulatoryReportResource(
            entity.Id.Value,
            entity.Code,
            entity.Type.ToString(),
            entity.Scope,
            entity.Owner,
            entity.Status.ToString(),
            entity.Template,
            entity.Format,
            entity.CreatedOn);
    }
}
