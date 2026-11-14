using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Manufacturing.Domain.Model.Entities;
using IngesCompanyCCPlatform.API.Manufacturing.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using IngesCompanyCCPlatform.API.Monitoring.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Organizations.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using IngesCompanyCCPlatform.API.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;


namespace IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Facility> Facilities => Set<Facility>();
    public DbSet<ContactInquiry> ContactInquiries => Set<ContactInquiry>();
    public DbSet<AdministrativeActivity> AdministrativeActivities => Set<AdministrativeActivity>();
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
    ///     Stores every <see cref="DateOnly" /> as a MySQL <c>date</c> through <see cref="DateOnlyConverter" />.
    /// </summary>
    /// <param name="configurationBuilder">The conventions of the model.</param>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateOnly>().HaveConversion<DateOnlyConverter>().HaveColumnType("date");
        base.ConfigureConventions(configurationBuilder);
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
        builder.ApplyConfiguration(new ContactInquiryEntityTypeConfiguration());
        builder.ApplyConfiguration(new AdministrativeActivityEntityTypeConfiguration());
        

        // Manufacturing Context
        builder.ApplyManufacturingConfiguration();
        builder.ApplyManufacturingCatalogConfiguration();

        // Subscriptions Context
        builder.ApplySubscriptionsConfiguration();

        // Monitoring Context
        builder.ApplyMonitoringConfiguration();

        // General naming convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}
