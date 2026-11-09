using IngesCompanyCCPlatform.API.Quality.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Quality.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Quality.Interfaces.Rest.Transform;

/// <summary>
///     Converts an <see cref="AuditTrailEntry" /> aggregate into an <see cref="AuditTrailEntryResource" />.
/// </summary>
public static class AuditTrailEntryResourceFromEntityAssembler
{
    public static AuditTrailEntryResource ToResourceFromEntity(AuditTrailEntry entity)
    {
        return new AuditTrailEntryResource(entity.Id, entity.EntityType, entity.EntityId, entity.Action.ToString(),
            entity.OldValue, entity.NewValue, entity.ChangedBy, entity.ChangedAt, entity.Reason);
    }
}

/// <summary>
///     Converts a <see cref="RecordAuditTrailEntryResource" /> into a <see cref="RecordAuditTrailEntryCommand" />.
/// </summary>
public static class RecordAuditTrailEntryCommandFromResourceAssembler
{
    public static RecordAuditTrailEntryCommand ToCommandFromResource(RecordAuditTrailEntryResource resource)
    {
        return new RecordAuditTrailEntryCommand(resource.EntityType, resource.EntityId,
            QualityEnumParser.Parse<EAuditAction>(resource.Action, "audit action"), resource.OldValue,
            resource.NewValue, new UserId(resource.ChangedBy), resource.Reason);
    }
}
