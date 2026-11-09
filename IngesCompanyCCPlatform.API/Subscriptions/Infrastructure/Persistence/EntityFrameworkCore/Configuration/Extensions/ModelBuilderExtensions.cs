using IngesCompanyCCPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.Aggregates;
using IngesCompanyCCPlatform.API.Subscriptions.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace IngesCompanyCCPlatform.API.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Entity Framework Core configuration of the Subscriptions bounded context.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the mapping of the <see cref="Plan" /> and <see cref="Subscription" /> aggregates.
    /// </summary>
    /// <param name="builder">The model builder of the database context.</param>
    public static void ApplySubscriptionsConfiguration(this ModelBuilder builder)
    {
        // Subscriptions Context
        builder.Entity<Plan>().HasKey(p => p.Id);
        builder.Entity<Plan>().Property(p => p.Id)
            .HasConversion(new StronglyTypedIdConverter<PlanId, int>(id => id.Value, value => new PlanId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Plan>().Property(p => p.PlanKey)
            .HasConversion(planKey => planKey.Value, value => new PlanKey(value))
            .IsRequired()
            .HasMaxLength(64);
        builder.Entity<Plan>().HasIndex(p => p.PlanKey).IsUnique();
        builder.Entity<Plan>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Plan>().ComplexProperty(p => p.MonthlyPrice, price =>
        {
            price.Property(m => m.Amount).HasColumnName("monthly_price_amount").HasPrecision(10, 2).IsRequired();
            price.Property(m => m.Currency).HasColumnName("monthly_price_currency").HasMaxLength(3).IsRequired();
        });
        builder.Entity<Plan>().ComplexProperty(p => p.AnnualPrice, price =>
        {
            price.Property(m => m.Amount).HasColumnName("annual_price_amount").HasPrecision(10, 2).IsRequired();
            price.Property(m => m.Currency).HasColumnName("annual_price_currency").HasMaxLength(3).IsRequired();
        });
        builder.Entity<Plan>().Property(p => p.UserLimit).IsRequired();
        builder.Entity<Plan>().Property(p => p.IotDeviceLimit).IsRequired();

        builder.Entity<Subscription>().HasKey(s => s.Id);
        builder.Entity<Subscription>().Property(s => s.Id)
            .HasConversion(new StronglyTypedIdConverter<SubscriptionId, int>(id => id.Value, value => new SubscriptionId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Subscription>().Property(s => s.OrganizationId)
            .HasConversion(new StronglyTypedIdConverter<OrganizationId, int>(id => id.Value, value => new OrganizationId(value)))
            .IsRequired();
        builder.Entity<Subscription>().Property(s => s.PlanId)
            .HasConversion(new StronglyTypedIdConverter<PlanId, int>(id => id.Value, value => new PlanId(value)))
            .IsRequired();
        builder.Entity<Subscription>().Property(s => s.BillingCycle).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<Subscription>().Property(s => s.Status).HasConversion<string>().HasMaxLength(16).IsRequired();
        builder.Entity<Subscription>().Property(s => s.AutoRenew).IsRequired();
        builder.Entity<Subscription>().ComplexProperty(s => s.PaymentCard, card =>
        {
            card.Property(c => c.Brand).HasColumnName("card_brand").HasMaxLength(30).IsRequired();
            card.Property(c => c.Last4).HasColumnName("card_last4").HasMaxLength(4).IsRequired();
            card.Property(c => c.Expiry).HasColumnName("card_expiry").HasMaxLength(7).IsRequired();
        });
        builder.Entity<Subscription>().Property(s => s.BillingEmail)
            .HasConversion(email => email.Value, value => new BillingEmail(value))
            .IsRequired()
            .HasMaxLength(254);
        builder.Entity<Subscription>().Property(s => s.CurrentPeriodEnd);
        builder.Entity<Subscription>().HasIndex(s => s.OrganizationId);
    }
}
