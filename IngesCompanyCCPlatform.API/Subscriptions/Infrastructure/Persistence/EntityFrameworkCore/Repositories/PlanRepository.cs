using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the plan repository of the Subscriptions bounded context.
/// </summary>
/// <param name="context">
///     The <see cref="AppDbContext" /> to use.
/// </param>
public class PlanRepository(AppDbContext context) : BaseRepository<Plan>(context), IPlanRepository
{
    // <inheritdoc />
    public async Task<bool> ExistsByPlanKeyAsync(PlanKey planKey, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Plan>().AnyAsync(plan => plan.PlanKey == planKey, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<Plan?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var planId = new PlanId(id);
        return await Context.Set<Plan>().FirstOrDefaultAsync(plan => plan.Id == planId, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<Plan?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default)
    {
        var planId = new PlanId(id);
        return await Context.Set<Plan>()
            .AsNoTracking()
            .FirstOrDefaultAsync(plan => plan.Id == planId, cancellationToken);
    }
}
