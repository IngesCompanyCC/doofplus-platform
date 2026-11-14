using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Organizations.Application.CommandServices;

/// <summary>
///     Application service for Organization write use cases.
/// </summary>
public interface IOrganizationCommandService
{
    /// <summary>
    ///     Registers a new organization (US50).
    /// </summary>
    Task<Result<Organization>> Handle(
        RegisterOrganizationCommand command,
        CancellationToken cancellationToken = default);

    Task<Result<Facility>> Handle(
        AddFacilityCommand command,
        CancellationToken cancellationToken = default);
}