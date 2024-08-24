using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects.TypeIds;

namespace Ordering.Infrastructure.Data.Configurations;
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> orderItemType)
    {
        orderItemType.HasKey(oi => oi.Id);

        orderItemType.Property(oi => oi.Id).HasConversion(
                                   orderItemId => orderItemId.Value,
                                   dbId => OrderItemId.Of(dbId));

        orderItemType.HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        orderItemType.Property(oi => oi.Quantity).IsRequired();

        orderItemType.Property(oi => oi.Price).IsRequired();
    }
}
