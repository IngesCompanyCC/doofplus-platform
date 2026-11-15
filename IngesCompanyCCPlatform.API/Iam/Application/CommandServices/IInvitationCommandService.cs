using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Commands;
using IngesCompanyCCPlatform.API.Shared.Application.Model;

namespace IngesCompanyCCPlatform.API.Iam.Application.CommandServices;

/// <summary>
///     Command service of <see cref="Invitation" />.
/// </summary>
public interface IInvitationCommandService
{
    /// <summary>
    ///     Sends an invitation to a new member of the laboratory.
    /// </summary>
    Task<Result<Invitation>> Handle(SendInvitationCommand command, CancellationToken cancellationToken = default);
}
