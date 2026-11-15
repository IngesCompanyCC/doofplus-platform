using IngesCompanyCCPlatform.API.Iam.Application.QueryServices;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.Queries;
using IngesCompanyCCPlatform.API.Iam.Domain.Repositories;

namespace IngesCompanyCCPlatform.API.Iam.Application.Internal.QueryServices;

/// <summary>
///     Query service of <see cref="Invitation" />.
/// </summary>
public class InvitationQueryService(IInvitationRepository invitationRepository) : IInvitationQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Invitation>> Handle(GetAllInvitationsQuery query, CancellationToken cancellationToken = default)
    {
        return query.OrganizationId is null
            ? await invitationRepository.ListAsNoTrackingAsync(cancellationToken)
            : await invitationRepository.FindAllByOrganizationIdAsync(query.OrganizationId.Value, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Invitation?> Handle(GetInvitationByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await invitationRepository.FindByIdAsNoTrackingAsync(query.InvitationId, cancellationToken);
    }
}
