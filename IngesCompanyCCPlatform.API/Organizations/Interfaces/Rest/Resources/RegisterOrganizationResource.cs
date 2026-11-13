namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

public record RegisterOrganizationResource(
    string LegalName,
    string Ruc,
    string FacilityName,
    string PlanKey,
    string BillingCycle,
    string AdministratorName,
    string AdministratorEmail,
    string Password);