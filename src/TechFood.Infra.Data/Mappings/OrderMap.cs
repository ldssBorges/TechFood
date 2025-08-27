using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Common.DTO;

namespace TechFood.Infra.Data.Mappings;

public class OrderMap : IEntityTypeConfiguration<OrderDTO>
{
    public void Configure(EntityTypeBuilder<OrderDTO> builder)
    {
        builder.ToTable("Order");

        builder.HasOne<CustomerDTO>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired(false);

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .IsRequired();

        builder.HasMany(o => o.Historical)
            .WithOne()
            .HasForeignKey("OrderId")
            .IsRequired();
    }
}
