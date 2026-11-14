using IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Model;
using IngesCompanyCCPlatform.API.Shared.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Organizations.Application.Internal.CommandServices;

/// <summary>
///     Handles organization write use cases (US50).
/// </summary>
public class OrganizationCommandService(
    IOrganizationRepository organizationRepository,
    IFacilityRepository facilityRepository,
    IAdministrativeActivityRepository administrativeActivityRepository,
    IUnitOfWork unitOfWork) : IOrganizationCommandService
{
    public async Task<Result<Organization>> Handle(
        RegisterOrganizationCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.LegalName))
            return Result<Organization>.Failure(OrganizationsError.InvalidOperation, "Legal name is required.");

        if (string.IsNullOrWhiteSpace(command.Ruc))
            return Result<Organization>.Failure(OrganizationsError.InvalidOperation, "RUC is required.");

        if (string.IsNullOrWhiteSpace(command.AdministratorName))
            return Result<Organization>.Failure(OrganizationsError.InvalidOperation, "Administrator name is required.");

        if (string.IsNullOrWhiteSpace(command.AdministratorEmail))
            return Result<Organization>.Failure(OrganizationsError.InvalidOperation, "Administrator email is required.");

        if (string.IsNullOrWhiteSpace(command.Password))
            return Result<Organization>.Failure(OrganizationsError.InvalidOperation, "Password is required.");

        Ruc ruc;
        try
        {
            ruc = new Ruc(command.Ruc);
        }
        catch (ArgumentException ex)
        {
            return Result<Organization>.Failure(OrganizationsError.InvalidOperation, ex.Message);
        }

        if (await organizationRepository.ExistsByRucAsync(ruc.Value, cancellationToken))
            return Result<Organization>.Failure(OrganizationsError.DuplicateRuc, "An organization with this RUC already exists.");

        var organization = new Organization(command);

        await organizationRepository.AddAsync(organization, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(command.FacilityName))
        {
            var facility = new Facility(
                organization.Id,
                command.FacilityName,
                FacilityType.Manufacturing,
                Facility.DefaultTimezone,
                command.AdministratorName);

            await facilityRepository.AddAsync(facility, cancellationToken);
        }

        await administrativeActivityRepository.AddAsync(
            new AdministrativeActivity(organization.Id, "Organization registered", organization.OwnerName),
            cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<Organization>.Success(organization);
    }

    public async Task<Result<Facility>> Handle(
        AddFacilityCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            return Result<Facility>.Failure(OrganizationsError.InvalidOperation, "Facility name is required.");

        var organization = await organizationRepository.FindByIdAsync(command.OrganizationId, cancellationToken);
        if (organization is null)
            return Result<Facility>.Failure(OrganizationsError.OrganizationNotFound,
                $"Organization with ID {command.OrganizationId} was not found.");

        var name = command.Name.Trim();
        if (await facilityRepository.ExistsByOrganizationIdAndNameAsync(organization.Id, name, cancellationToken))
            return Result<Facility>.Failure(OrganizationsError.DuplicateFacilityName,
                "The organization already has a facility with this name.");

        var ownerName = string.IsNullOrWhiteSpace(command.OwnerName) ? organization.OwnerName : command.OwnerName;
        var facility = new Facility(organization.Id, name, command.Type, command.Timezone ?? string.Empty, ownerName);

        await facilityRepository.AddAsync(facility, cancellationToken);
        await administrativeActivityRepository.AddAsync(
            new AdministrativeActivity(organization.Id, $"Facility {facility.Name} added", ownerName),
            cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        return Result<Facility>.Success(facility);
    }
}
