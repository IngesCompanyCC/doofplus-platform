using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Facility> Facilities => Set<Facility>();
    public DbSet<ProductionBatch> ProductionBatches => Set<ProductionBatch>();
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<MaterialConsumption> MaterialConsumptions => Set<MaterialConsumption>();
    public DbSet<BatchEvent> BatchEvents => Set<BatchEvent>();

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
        
        builder.ApplyConfiguration(new OrganizationEntityTypeConfiguration());
        builder.ApplyConfiguration(new UserProfileEntityTypeConfiguration());
        builder.ApplyConfiguration(new FacilityEntityTypeConfiguration());
        

        // Manufacturing Context
        builder.ApplyManufacturingConfiguration();

        // General naming convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}
