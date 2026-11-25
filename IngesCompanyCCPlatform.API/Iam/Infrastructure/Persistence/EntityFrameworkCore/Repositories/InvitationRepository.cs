using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Iam.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of <see cref="Invitation" />.
/// </summary>
public class InvitationRepository(AppDbContext context) : BaseRepository<Invitation>(context), IInvitationRepository
{
    /// <inheritdoc />
    public async Task<bool> ExistsPendingByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalized = email.Trim().ToLowerInvariant();
        return await Context.Set<Invitation>()
            .AnyAsync(invitation => invitation.Email == normalized && invitation.Status == EInvitationStatus.Pending, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Invitation?> FindPendingByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalized = email.Trim().ToLowerInvariant();
        return await Context.Set<Invitation>()
            .FirstOrDefaultAsync(invitation => invitation.Email == normalized && invitation.Status == EInvitationStatus.Pending, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Invitation>> FindAllByOrganizationIdAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Invitation>()
            .AsNoTracking()
            .Where(invitation => invitation.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }
}
