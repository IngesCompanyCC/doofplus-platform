using IngesCompanyCCPlatform.API.Organizations.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IngesCompanyCCPlatform.API.Organizations.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class ContactInquiryEntityTypeConfiguration : IEntityTypeConfiguration<ContactInquiry>
{
    public void Configure(EntityTypeBuilder<ContactInquiry> builder)
    {
        builder.ToTable("contact_inquiries");

        builder.HasKey(inquiry => inquiry.Id);

        builder.Property(inquiry => inquiry.Id)
            .IsRequired();

        builder.Property(inquiry => inquiry.FullName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(inquiry => inquiry.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(inquiry => inquiry.Message)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(inquiry => inquiry.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(inquiry => inquiry.ReceivedAt)
            .IsRequired();

        builder.Property(inquiry => inquiry.CreatedAt);
        builder.Property(inquiry => inquiry.UpdatedAt);
    }
}