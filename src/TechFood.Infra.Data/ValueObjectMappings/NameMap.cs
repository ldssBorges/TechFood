using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Common.DTO.ValueObjects;

namespace TechFood.Infra.Data.ValueObjectMappings;

public static class NameMap
{
    public static void MapName<TEntity>(this OwnedNavigationBuilder<TEntity, NameDTO> navigationBuilder)
        where TEntity : class
    {
        navigationBuilder.WithOwner();

        navigationBuilder.Property(x => x.FullName)
            .HasMaxLength(255)
            .HasColumnName("NameFullName")
            .IsRequired();
    }
}
