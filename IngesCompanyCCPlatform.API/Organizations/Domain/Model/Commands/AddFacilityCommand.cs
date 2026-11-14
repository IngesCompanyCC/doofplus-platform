using IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;

namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;

/// <summary>
///     Command to add a facility (plant) to an organization.
/// </summary>
/// <param name="OrganizationId">The organization that operates the facility.</param>
/// <param name="Name">The name of the facility.</param>
/// <param name="Type">Manufacturing plant or QC laboratory.</param>
/// <param name="Timezone">The time zone of the facility; America/Lima when it is empty.</param>
/// <param name="OwnerName">The person in charge of the facility.</param>
public record AddFacilityCommand(int OrganizationId, string Name, FacilityType Type, string? Timezone, string? OwnerName);
