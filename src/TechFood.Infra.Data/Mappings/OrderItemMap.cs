using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Common.DTO;

namespace TechFood.Infra.Data.Mappings;

public class OrderItemMap : IEntityTypeConfiguration<OrderItemDTO>
{
    public void Configure(EntityTypeBuilder<OrderItemDTO> builder)
    {
        builder.ToTable("OrderItem");

        builder.HasOne<ProductDTO>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
