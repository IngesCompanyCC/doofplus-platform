using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Queries;

namespace IngesCompanyCCPlatform.API.Iam.Application.QueryServices;

/// <summary>
///     Query service of <see cref="Invitation" />.
/// </summary>
public interface IInvitationQueryService
{
    /// <summary>
    ///     Gets the invitations, of one organization when it is sent.
    /// </summary>
    Task<IEnumerable<Invitation>> Handle(GetAllInvitationsQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets an invitation by its id, or <c>null</c>.
    /// </summary>
    Task<Invitation?> Handle(GetInvitationByIdQuery query, CancellationToken cancellationToken = default);
}
