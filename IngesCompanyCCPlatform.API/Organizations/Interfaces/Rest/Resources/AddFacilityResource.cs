namespace IngesCompanyCCPlatform.API.Organizations.Interfaces.Rest.Resources;

/// <summary>
///     Request to add a facility to an organization.
/// </summary>
/// <param name="OrganizationId">The organization.</param>
/// <param name="Name">The name of the facility.</param>
/// <param name="Type">Manufacturing or QcLaboratory.</param>
/// <param name="Timezone">The time zone; America/Lima when it is empty.</param>
/// <param name="OwnerName">The person in charge.</param>
public record AddFacilityResource(int OrganizationId, string Name, string Type, string? Timezone, string? OwnerName);
