using IngesCompanyCCPlatform.API.Iam.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Iam.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        var user = builder.Entity<User>();
        user.HasKey(u => u.Id);
        user.Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        user.Property(u => u.Email).IsRequired().HasMaxLength(120);
        user.HasIndex(u => u.Email).IsUnique();
        user.Property(u => u.PasswordHash).IsRequired().HasMaxLength(120);
        user.Property(u => u.FullName).IsRequired().HasMaxLength(120);
        user.Property(u => u.Role).IsRequired().HasMaxLength(40)
            .HasConversion(role => role.ToName(), name => FromName(name));
        user.Property(u => u.Environment).IsRequired().HasMaxLength(20);
        user.Property(u => u.Facility).IsRequired().HasMaxLength(100);
        user.Property(u => u.Status).IsRequired().HasMaxLength(20);
        user.Property(u => u.Privilege).IsRequired().HasMaxLength(30);
        user.Property(u => u.OrganizationId).IsRequired();
        user.HasIndex(u => u.OrganizationId);
        user.Property(u => u.OrganizationName).IsRequired().HasMaxLength(150);
        user.Property(u => u.Plant).IsRequired().HasMaxLength(100);
        user.Property(u => u.TwoFactorCodeHash).HasMaxLength(120);
        user.Ignore(u => u.HasTwoFactor);
        user.Ignore(u => u.Initials);
    }

    private static Roles FromName(string name) =>
        RolesExtensions.TryFromKey(name, out var role) ? role : Roles.RoleQaSpecialist;
}
