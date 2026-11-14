using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;

/// <summary>
///     Converts an <see cref="AdministrativeActivity" /> to its resource.
/// </summary>
public static class AdministrativeActivityResourceFromEntityAssembler
{
    public static AdministrativeActivityResource ToResourceFromEntity(AdministrativeActivity entity)
    {
        return new AdministrativeActivityResource(entity.Id, entity.OrganizationId, entity.Title, entity.Actor, entity.OccurredAt);
    }
}
