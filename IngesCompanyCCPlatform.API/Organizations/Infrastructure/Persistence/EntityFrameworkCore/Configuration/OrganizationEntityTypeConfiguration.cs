using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IngesCompanyCCPlatform.API.Organizations.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class OrganizationEntityTypeConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("organizations");

        builder.HasKey(organization => organization.Id);

        builder.Property(organization => organization.Id)
            .IsRequired();

        builder.Property(organization => organization.LegalName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(organization => organization.Ruc)
            .IsRequired()
            .HasMaxLength(11);

        builder.HasIndex(organization => organization.Ruc)
            .IsUnique();

        builder.Property(organization => organization.Region)
            .HasMaxLength(100);

        builder.Property(organization => organization.OwnerName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(organization => organization.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(organization => organization.CreatedAt);
        builder.Property(organization => organization.UpdatedAt);
    }
}