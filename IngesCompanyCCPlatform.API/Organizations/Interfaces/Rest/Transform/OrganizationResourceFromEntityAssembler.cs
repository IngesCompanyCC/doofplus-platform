using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;

public static class OrganizationResourceFromEntityAssembler
{
    public static OrganizationResource ToResourceFromEntity(Organization entity)
    {
        return new OrganizationResource(
            entity.Id,
            entity.LegalName,
            entity.Ruc,
            entity.Region,
            entity.OwnerName,
            entity.Status.ToString(),
            entity.CreatedAt);
    }
}