using IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Application.Patterns;
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
    public async Task<Result<Organization, string>> Handle(
        RegisterOrganizationCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.LegalName))
            return new Result<Organization, string>.Failure("Legal name is required.");

        if (string.IsNullOrWhiteSpace(command.Ruc))
            return new Result<Organization, string>.Failure("RUC is required.");

        if (string.IsNullOrWhiteSpace(command.AdministratorName))
            return new Result<Organization, string>.Failure("Administrator name is required.");

        if (string.IsNullOrWhiteSpace(command.AdministratorEmail))
            return new Result<Organization, string>.Failure("Administrator email is required.");

        if (string.IsNullOrWhiteSpace(command.Password))
            return new Result<Organization, string>.Failure("Password is required.");

        Ruc ruc;
        try
        {
            ruc = new Ruc(command.Ruc);
        }
        catch (ArgumentException ex)
        {
            return new Result<Organization, string>.Failure(ex.Message);
        }

        if (await organizationRepository.ExistsByRucAsync(ruc.Value, cancellationToken))
            return new Result<Organization, string>.Failure("An organization with this RUC already exists.");

        var organization = new Organization(command);

        await organizationRepository.AddAsync(organization, cancellationToken);

        if (!string.IsNullOrWhiteSpace(command.FacilityName))
        {
            var facility = new Facility(
                organization.Id,
                command.FacilityName,
                FacilityType.Manufacturing,
                "UTC",
                command.AdministratorName);

            await facilityRepository.AddAsync(facility, cancellationToken);
        }

        await unitOfWork.CompleteAsync(cancellationToken);

        // Domain event available for IAM integration later:
        // var domainEvent = organization.ToRegisteredEvent(command);

        return new Result<Organization, string>.Success(organization);
    }
}