using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using IngesCompanyCCPlatform.API.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Application database context for the DoofPlus platform.
/// </summary>
/// <remarks>
///     The audit timestamps are applied here. The domain events are published by
///     <see cref="DispatchDomainEventsInterceptor" />, which needs the mediator and is therefore
///     added from the dependency injection container in <c>Program.cs</c>.
/// </remarks>
/// <param name="options">The options for the database context.</param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Apply audit timestamps for all IAuditableEntity implementations
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <summary>
    ///     Creates the database model. Each bounded context applies its own configuration here.
    /// </summary>
    /// <param name="builder">The model builder for the database context.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Subscriptions Context
        builder.ApplySubscriptionsConfiguration();

        // General naming convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}
