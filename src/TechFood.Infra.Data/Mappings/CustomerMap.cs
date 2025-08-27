using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Common.DTO;
using TechFood.Infra.Data.ValueObjectMappings;

namespace TechFood.Infra.Data.Mappings;

public class CustomerMap : IEntityTypeConfiguration<CustomerDTO>
{
    public void Configure(EntityTypeBuilder<CustomerDTO> builder)
    {
        builder.ToTable("Customer");

        builder.OwnsOne(a => a.Name, name => name.MapName())
            .Navigation(a => a.Name)
            .IsRequired();

        builder.OwnsOne(x => x.Email, email => email.MapEmail())
            .Navigation(x => x.Email)
            .IsRequired();

        builder.OwnsOne(x => x.Document, document => document.MapDocument())
            .Navigation(x => x.Document)
            .IsRequired();

        builder.OwnsOne(x => x.Phone, phone => phone!.MapPhone())
            .Navigation(x => x.Phone);
    }
}
