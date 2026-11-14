using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IngesCompanyCCPlatform.API.Organizations.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class FacilityEntityTypeConfiguration : IEntityTypeConfiguration<Facility>
{
    public void Configure(EntityTypeBuilder<Facility> builder)
    {
        builder.ToTable("facilities");

        builder.HasKey(facility => facility.Id);

        builder.Property(facility => facility.Id)
            .IsRequired();

        builder.Property(facility => facility.OrganizationId)
            .IsRequired();

        builder.Property(facility => facility.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(facility => facility.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(facility => facility.Timezone)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(facility => facility.OwnerName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(facility => facility.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(facility => facility.CreatedAt);
        builder.Property(facility => facility.UpdatedAt);

        builder.HasIndex(facility => facility.OrganizationId);
    }
}