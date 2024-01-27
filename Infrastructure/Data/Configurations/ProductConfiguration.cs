using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ProductCode)
            .HasConversion(
            v => v.Code, 
            v => new ProductCode(v))
            .IsRequired();

        builder.HasIndex(p => p.ProductCode)
            .IsUnique();
    }
}
