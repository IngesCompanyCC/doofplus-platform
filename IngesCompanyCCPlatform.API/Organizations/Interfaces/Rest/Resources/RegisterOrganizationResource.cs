namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

public record RegisterOrganizationResource(
    string LegalName,
    string Ruc,
    string? Region,
    string OwnerName);