using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Builds the commands of <c>RegulatoryReport</c> from the resources.
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
            resource.Code,
            resource.Title,
            resource.Description ?? string.Empty,
            resource.RegulatoryAuthority ?? string.Empty,
            resource.FileReference ?? string.Empty,
            resource.DueDate,
            resource.PreparedBy);
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
            entity.Title,
            entity.Description,
            entity.RegulatoryAuthority,
            entity.FileReference,
            entity.DueDate,
            entity.PreparedBy,
            entity.Status.ToString(),
            entity.SubmittedAt is null ? null : DateTime.SpecifyKind(entity.SubmittedAt.Value, DateTimeKind.Utc));
    }
}
