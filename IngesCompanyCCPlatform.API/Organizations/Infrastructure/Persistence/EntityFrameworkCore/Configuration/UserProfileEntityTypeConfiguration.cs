using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IngesCompanyCCPlatform.API.Organizations.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("user_profiles");

        builder.HasKey(profile => profile.Id);

        builder.Property(profile => profile.Id)
            .IsRequired();

        builder.Property(profile => profile.OrganizationId)
            .IsRequired();

        builder.Property(profile => profile.UserId)
            .IsRequired();

        builder.Property(profile => profile.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(profile => profile.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(profile => profile.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(profile => profile.Email)
            .IsUnique();

        builder.Property(profile => profile.Area)
            .HasMaxLength(100);

        builder.Property(profile => profile.Site)
            .HasMaxLength(100);

        builder.Property(profile => profile.Role)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(profile => profile.EmailNotifications)
            .IsRequired();

        builder.Property(profile => profile.InAppNotifications)
            .IsRequired();

        builder.Property(profile => profile.Language)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(profile => profile.IsActive)
            .IsRequired();

        builder.Property(profile => profile.CreatedAt);
        builder.Property(profile => profile.UpdatedAt);

        builder.Ignore(profile => profile.FullName);

        builder.HasIndex(profile => profile.OrganizationId);
        builder.HasIndex(profile => profile.UserId);
    }
}