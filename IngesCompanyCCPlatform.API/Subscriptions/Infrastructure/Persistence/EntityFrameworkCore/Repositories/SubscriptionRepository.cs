using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the subscription repository of the Subscriptions bounded context.
/// </summary>
/// <param name="context">
///     The <see cref="AppDbContext" /> to use.
/// </param>
public class SubscriptionRepository(AppDbContext context)
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    // <inheritdoc />
    public async Task<IEnumerable<Subscription>> FindAllByOrganizationIdAsNoTrackingAsync(
        OrganizationId organizationId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Subscription>()
            .AsNoTracking()
            .Include(subscription => subscription.Payments)
            .Where(subscription => subscription.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    // <inheritdoc />
    public async Task<bool> ExistsByOrganizationIdAndStatusInAsync(OrganizationId organizationId,
        IEnumerable<ESubscriptionStatus> statuses, CancellationToken cancellationToken = default)
    {
        var states = statuses.ToList();
        return await Context.Set<Subscription>().AnyAsync(
            subscription => subscription.OrganizationId == organizationId && states.Contains(subscription.Status),
            cancellationToken);
    }

    // <inheritdoc />
    public new async Task<Subscription?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var subscriptionId = new SubscriptionId(id);
        return await Context.Set<Subscription>()
            .Include(subscription => subscription.Payments)
            .FirstOrDefaultAsync(subscription => subscription.Id == subscriptionId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<Subscription?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var subscriptionId = new SubscriptionId(id);
        return await Context.Set<Subscription>()
            .AsNoTracking()
            .Include(subscription => subscription.Payments)
            .FirstOrDefaultAsync(subscription => subscription.Id == subscriptionId, cancellationToken);
    }
}
