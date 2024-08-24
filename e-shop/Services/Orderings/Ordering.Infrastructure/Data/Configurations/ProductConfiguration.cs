using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> productType)
    {
        productType.HasKey(p => p.Id);

        productType.Property(p => p.Id).HasConversion(productId => productId.Value, dbId => ProductId.Of(dbId));
        //productType.Property(p => p.Id).HasConversion(productId => productId.Value, dbId => dbId);

        productType.Property(p => p.Name).HasMaxLength(100).IsRequired();
    }
}
