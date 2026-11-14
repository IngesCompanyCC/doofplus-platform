using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IngesCompanyCCPlatform.API.Organizations.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Mapping of <see cref="AdministrativeActivity" /> to the <c>administrative_activities</c> table.
/// </summary>
public class AdministrativeActivityEntityTypeConfiguration : IEntityTypeConfiguration<AdministrativeActivity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AdministrativeActivity> builder)
    {
        builder.ToTable("administrative_activities");
        builder.HasKey(activity => activity.Id);
        builder.Property(activity => activity.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(activity => activity.OrganizationId).IsRequired();
        builder.Property(activity => activity.Title).IsRequired().HasMaxLength(200);
        builder.Property(activity => activity.Actor).IsRequired().HasMaxLength(150);
        builder.Property(activity => activity.OccurredAt).IsRequired();
        builder.HasIndex(activity => activity.OrganizationId);
    }
}
