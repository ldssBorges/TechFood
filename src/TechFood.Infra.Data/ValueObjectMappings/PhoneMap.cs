using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Common.DTO.ValueObjects;

namespace TechFood.Infra.Data.ValueObjectMappings;

public static class PhoneMap
{
    public static void MapPhone<TEntity>(this OwnedNavigationBuilder<TEntity, PhoneDTO> navigationBuilder)
       where TEntity : class
    {
        navigationBuilder.WithOwner();

        navigationBuilder.Property(x => x.Number)
            .HasMaxLength(15)
            .HasColumnName("PhoneNumber")
            .IsRequired();

        navigationBuilder.Property(x => x.DDD)
            .HasMaxLength(4)
            .HasColumnName("PhoneDDD")
            .IsRequired();

        navigationBuilder.Property(x => x.CountryCode)
            .HasMaxLength(5)
            .HasColumnName("PhoneCountryCode")
            .IsRequired();
    }
}
