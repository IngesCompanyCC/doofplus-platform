using IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Entities;

namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;

/// <summary>
///     Facility aggregate root.
///     Represents a plant or laboratory site belonging to an organization.
/// </summary>
public class Facility : IAuditableEntity
{
    public const string DefaultTimezone = "America/Lima";

    public int Id { get; private set; }
    public int OrganizationId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public FacilityType Type { get; private set; }
    public string Timezone { get; private set; } = DefaultTimezone;
    public string OwnerName { get; private set; } = string.Empty;
    public FacilityStatus Status { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }


    private Facility()
    {
    }

    public Facility(
        int organizationId,
        string name,
        FacilityType type,
        string timezone,
        string ownerName)
    {
        OrganizationId = organizationId;
        Name = name.Trim();
        Type = type;
        Timezone = string.IsNullOrWhiteSpace(timezone) ? DefaultTimezone : timezone.Trim();
        OwnerName = ownerName.Trim();
        Status = FacilityStatus.Active;
    }

    public void Deactivate()
    {
        Status = FacilityStatus.Inactive;
    }

    public void Activate()
    {
        Status = FacilityStatus.Active;
    }
}