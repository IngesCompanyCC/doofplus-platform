namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

public record OrganizationResource(
    Guid Id,
    string LegalName,
    string Ruc,
    string Region,
    string OwnerName,
    string Status,
    DateTimeOffset? CreatedAt);