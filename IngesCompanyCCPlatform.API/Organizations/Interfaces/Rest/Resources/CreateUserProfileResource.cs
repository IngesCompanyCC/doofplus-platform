namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

public record CreateUserProfileResource(
    int OrganizationId,
    int UserId,
    string FirstName,
    string LastName,
    string Email,
    string Area,
    string Site,
    string Language = "en");