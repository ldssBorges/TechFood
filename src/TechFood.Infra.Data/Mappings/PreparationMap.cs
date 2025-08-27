using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Common.DTO;

namespace TechFood.Infra.Data.Mappings;

public class PreparationMap : IEntityTypeConfiguration<PreparationDTO>
{
    public void Configure(EntityTypeBuilder<PreparationDTO> builder)
    {
        builder.ToTable("Preparation");
    }
}

