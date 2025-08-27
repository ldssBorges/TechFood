using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Common.DTO;

namespace TechFood.Infra.Data.Mappings;

public class CategoryMap : IEntityTypeConfiguration<CategoryDTO>
{
    public void Configure(EntityTypeBuilder<CategoryDTO> builder)
    {
        builder.ToTable("Category");

        builder.Property(c => c.Name)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.ImageFileName)
            .HasMaxLength(50)
            .IsRequired();
    }
}
