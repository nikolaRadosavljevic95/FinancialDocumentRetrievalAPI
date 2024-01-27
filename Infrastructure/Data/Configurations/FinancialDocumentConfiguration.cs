using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations;

public class FinancialDocumentConfiguration : IEntityTypeConfiguration<FinancialDocument>
{
    public void Configure(EntityTypeBuilder<FinancialDocument> builder)
    {
        builder.HasKey(fd => fd.Id);

        builder.Property(fd => fd.AccountNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(fd => fd.Balance)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(fd => fd.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.HasOne(fd => fd.Tenant)
            .WithMany()
            .HasForeignKey(fd => fd.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(fd => fd.Client)
            .WithMany()
            .HasForeignKey(fd => fd.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
