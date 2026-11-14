using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Transform;

public static class RegisterOrganizationCommandFromResourceAssembler
{
    public static RegisterOrganizationCommand ToCommandFromResource(
        RegisterOrganizationResource resource)
    {
        return new RegisterOrganizationCommand(
            resource.LegalName,
            resource.Ruc,
            resource.FacilityName,
            resource.PlanKey,
            resource.BillingCycle,
            resource.AdministratorName,
            resource.AdministratorEmail,
            resource.Password);
    }
}