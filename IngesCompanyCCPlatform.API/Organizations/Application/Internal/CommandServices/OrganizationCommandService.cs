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
                "UTC",
                command.AdministratorName);

            await facilityRepository.AddAsync(facility, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
        }

        return Result<Organization>.Success(organization);
    }
}
