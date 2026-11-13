namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

public record OrganizationResource(
    int Id,
    string LegalName,
    string Ruc,
    string Region,
    string OwnerName,
    string Status,
    DateTimeOffset? CreatedAt);