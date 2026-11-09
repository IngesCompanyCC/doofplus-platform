using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Converts a <see cref="RegulatoryReport" /> aggregate into a <see cref="RegulatoryReportResource" />.
/// </summary>
public static class RegulatoryReportResourceFromEntityAssembler
{
    public static RegulatoryReportResource ToResourceFromEntity(RegulatoryReport entity)
    {
        return new RegulatoryReportResource(entity.Id, entity.OrganizationId, entity.Period.Value, entity.FileUrl,
            entity.GeneratedAt);
    }
}

/// <summary>
///     Converts a <see cref="RegisterRegulatoryReportResource" /> into a <see cref="RegisterRegulatoryReportCommand" />.
/// </summary>
public static class RegisterRegulatoryReportCommandFromResourceAssembler
{
    public static RegisterRegulatoryReportCommand ToCommandFromResource(RegisterRegulatoryReportResource resource)
    {
        return new RegisterRegulatoryReportCommand(new OrganizationId(resource.OrganizationId),
            new ReportPeriod(resource.Period), resource.FileUrl);
    }
}
