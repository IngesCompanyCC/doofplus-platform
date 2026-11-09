using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Converts an <see cref="Audit" /> aggregate into an <see cref="AuditResource" />.
/// </summary>
public static class AuditResourceFromEntityAssembler
{
    public static AuditResource ToResourceFromEntity(Audit entity)
    {
        return new AuditResource(entity.Id, entity.Type.ToString(), entity.ScheduledDate, entity.Status.ToString(),
            entity.EvidencePackageUrl,
            entity.Findings.OrderBy(finding => finding.Id).Select(finding =>
                new AuditFindingResource(finding.Id, finding.Type.ToString(), finding.Description)));
    }
}

/// <summary>
///     Converts the request resources of the audits into commands.
/// </summary>
public static class AuditCommandFromResourceAssembler
{
    public static ScheduleAuditCommand ToCommandFromResource(ScheduleAuditResource resource)
    {
        return new ScheduleAuditCommand(QualityEnumParser.Parse<EAuditType>(resource.Type, "audit type"),
            resource.ScheduledDate);
    }

    public static RegisterAuditFindingCommand ToCommandFromResource(int auditId, RegisterAuditFindingResource resource)
    {
        return new RegisterAuditFindingCommand(auditId,
            QualityEnumParser.Parse<EFindingType>(resource.Type, "finding type"), resource.Description);
    }

    public static AttachAuditEvidencePackageCommand ToCommandFromResource(int auditId,
        AttachAuditEvidencePackageResource resource)
    {
        return new AttachAuditEvidencePackageCommand(auditId, resource.FileUrl);
    }
}
