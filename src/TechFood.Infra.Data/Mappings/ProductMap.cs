using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Common.DTO;

namespace TechFood.Infra.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<ProductDTO>
{
    public void Configure(EntityTypeBuilder<ProductDTO> builder)
    {
        builder.ToTable("Product");

        builder.Property(a => a.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.Description)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne<CategoryDTO>()
            .WithMany()
            .IsRequired();

        builder.Property(a => a.ImageFileName)
            .HasMaxLength(50)
            .IsRequired();
    }
}
