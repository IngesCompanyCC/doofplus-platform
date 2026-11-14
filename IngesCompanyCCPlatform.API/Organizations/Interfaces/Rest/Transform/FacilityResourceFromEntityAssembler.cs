using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;

public static class FacilityResourceFromEntityAssembler
{
    public static FacilityResource ToResourceFromEntity(Facility entity)
    {
        return new FacilityResource(
            entity.Id,
            entity.OrganizationId,
            entity.Name,
            entity.Type.ToString(),
            entity.Timezone,
            entity.OwnerName,
            entity.Status.ToString(),
            entity.CreatedAt);
    }
}