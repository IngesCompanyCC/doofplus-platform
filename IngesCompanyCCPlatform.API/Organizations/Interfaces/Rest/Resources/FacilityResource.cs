namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

public record FacilityResource(
    int Id,
    int OrganizationId,
    string Name,
    string Type,
    string Timezone,
    string OwnerName,
    string Status,
    DateTimeOffset? CreatedAt);