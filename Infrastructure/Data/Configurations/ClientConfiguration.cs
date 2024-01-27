using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Vat)
            .HasColumnName("VAT")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.CompanyType)
           .IsRequired()
           .HasConversion(
               v => v.ToString(),
               v => (CompanyType)Enum.Parse(typeof(CompanyType), v))
           .HasMaxLength(20);

        builder.Property(c => c.IsWhitelisted)
            .IsRequired();

        builder.HasIndex(c => c.Vat)
            .IsUnique();
    }
}
