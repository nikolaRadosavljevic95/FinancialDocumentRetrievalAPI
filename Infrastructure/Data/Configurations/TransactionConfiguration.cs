using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Amount)
               .IsRequired()
               .HasColumnType("decimal(18, 2)");

        builder.Property(t => t.Date)
               .IsRequired();

        builder.Property(t => t.Description)
               .IsRequired()
               .HasMaxLength(250);

        builder.Property(t => t.Category)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(t => t.VendorName)
               .IsRequired()
               .HasMaxLength(100);
 
        builder.HasOne<FinancialDocument>()
               .WithMany(fd => fd.Transactions)
               .HasForeignKey("FinancialDocumentId");
    }
}
