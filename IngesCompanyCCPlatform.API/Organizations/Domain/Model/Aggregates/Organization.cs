using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Events;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Shared.Domain.Model.Entities;

namespace IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;

/// <summary>
///     Organization aggregate root.
///     Represents a pharmaceutical laboratory registered in DoofPlus.
/// </summary>
public class Organization : IAuditableEntity
{
    public int Id { get; private set; }
    public string LegalName { get; private set; } = string.Empty;
    public string Ruc { get; private set; } = string.Empty;
    public string Region { get; private set; } = string.Empty;
    public string OwnerName { get; private set; } = string.Empty;
    public OrganizationStatus Status { get; private set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    private Organization()
    {
    }

    public Organization(RegisterOrganizationCommand command)
    {
        LegalName = command.LegalName.Trim();
        Ruc = new Ruc(command.Ruc).Value;
        Region = string.Empty;
        OwnerName = command.AdministratorName.Trim();
        Status = OrganizationStatus.PendingVerification;
    }

    public void Activate()
    {
        Status = OrganizationStatus.Active;
    }

    public void Suspend()
    {
        Status = OrganizationStatus.Suspended;
    }

    public OrganizationRegisteredEvent ToRegisteredEvent(RegisterOrganizationCommand command)
    {
        return new OrganizationRegisteredEvent(
            Id,
            LegalName,
            Ruc,
            command.AdministratorEmail,
            command.AdministratorName,
            command.Password);
    }
}