using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Domain.Repositories;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Organizations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Entity Framework Core repository of the administrative activities.
/// </summary>
public class AdministrativeActivityRepository(AppDbContext context)
    : BaseRepository<AdministrativeActivity>(context), IAdministrativeActivityRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<AdministrativeActivity>> ListMostRecentFirstAsync(
        int? organizationId,
        CancellationToken cancellationToken = default)
    {
        var activities = Context.Set<AdministrativeActivity>().AsNoTracking();
        if (organizationId is not null)
            activities = activities.Where(activity => activity.OrganizationId == organizationId);
        return await activities.OrderByDescending(activity => activity.OccurredAt).ToListAsync(cancellationToken);
    }
}
