using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects.TypeIds;

namespace Ordering.Infrastructure.Data.Configurations;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> customerType)
    {
        customerType.HasKey(c => c.Id);

        customerType.Property(c => c.Id).HasConversion(customerId => customerId.Value, dbId => new(dbId));

        customerType.Property(c => c.Name).HasMaxLength(100).IsRequired();

        customerType.Property(c => c.Email).HasMaxLength(255);

        customerType.HasIndex(c => c.Email).IsUnique();
    }
}
