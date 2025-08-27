using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TechFood.Common.DTO;
using TechFood.Common.DTO.Enums;

namespace TechFood.Infra.Data.Contexts;

public class TechFoodContext(
    DbContextOptions<TechFoodContext> options) : DbContext(options)
{
    public DbSet<CategoryDTO> Categories { get; set; } = null!;

    public DbSet<CustomerDTO> Customers { get; set; } = null!;

    public DbSet<OrderDTO> Orders { get; set; } = null!;

    public DbSet<ProductDTO> Products { get; set; } = null!;

    public DbSet<PaymentDTO> Payments { get; set; } = null!;

    public DbSet<UserDTO> Users { get; set; } = null!;

    public DbSet<PreparationDTO> Preparations { get; set; } = null!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker
            .Entries<EntityDTO>()
            .Where(e => e.State == EntityState.Deleted))
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsDeleted = true;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TechFoodContext).Assembly);

        var properties = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(EntityDTO).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(EntityDTO.IsDeleted));
                var condition = Expression.Equal(property, Expression.Constant(false));
                var lambda = Expression.Lambda(condition, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);

                modelBuilder.Entity(entityType.ClrType)
                    .HasKey(nameof(EntityDTO.Id));

                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(EntityDTO.Id))
                    .IsRequired()
                    .ValueGeneratedNever();
            }
        }

        var stringProperties = properties.Where(p => p.ClrType == typeof(string));
        foreach (var property in stringProperties)
        {
            var maxLength = property.GetMaxLength() ?? 50;

            property.SetColumnType($"varchar({maxLength})");
        }

        var booleanProperties = properties
            .Where(p => p.ClrType == typeof(bool) ||
                        p.ClrType == typeof(bool?));

        foreach (var property in booleanProperties)
        {
            property.SetColumnType("bit");
            property.IsNullable = false;
        }

        var dateTimeProperties = properties.Where(p => p.ClrType == typeof(DateTime));

        foreach (var property in dateTimeProperties)
        {
            property.SetColumnType("datetime");
        }

        var enumProperties = properties.Where(p => p.ClrType == typeof(Enum));

        foreach (var property in enumProperties)
        {
            property.SetColumnType("smallint");
        }

        var amountProperties = properties
            .Where(p => p.ClrType == typeof(decimal) ||
                        p.ClrType == typeof(decimal?));

        foreach (var property in amountProperties)
        {
            property.SetColumnType("decimal(6, 2)");
        }

        SeedContext(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void SeedContext(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerDTO>()
            .HasData(
                new { Id = new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"), IsDeleted = false }
            );

        modelBuilder.Entity<CustomerDTO>().OwnsOne(c => c.Name)
           .HasData(
               new { CustomerDTOId = new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"), FullName = "John" }
           );

        modelBuilder.Entity<CustomerDTO>().OwnsOne(c => c.Document)
          .HasData(
              new { CustomerDTOId = new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"), Type = DocumentTypeDTO.CPF, Value = "4511554544" }
          );

        modelBuilder.Entity<CustomerDTO>().OwnsOne(c => c.Email)
          .HasData(
              new { CustomerDTOId = new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"), Address = "john.dev@gmail.com" }
          );

        modelBuilder.Entity<CustomerDTO>().OwnsOne(c => c.Phone)
          .HasData(
              new { CustomerDTOId = new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"), CountryCode = "55", DDD = "11", Number = "9415452222" }
          );

        modelBuilder.Entity<CustomerDTO>()
            .HasData(
                new { Id = new Guid("9887b301-605f-46a6-93db-ac1ce8685723"), IsDeleted = false }
            );

        modelBuilder.Entity<CustomerDTO>().OwnsOne(c => c.Name)
           .HasData(
               new { CustomerDTOId = new Guid("9887b301-605f-46a6-93db-ac1ce8685723"), FullName = "John Silva" }
           );

        modelBuilder.Entity<CustomerDTO>().OwnsOne(c => c.Document)
          .HasData(
              new { CustomerDTOId = new Guid("9887b301-605f-46a6-93db-ac1ce8685723"), Type = DocumentTypeDTO.CPF, Value = "000000000191" }
          );

        modelBuilder.Entity<CustomerDTO>().OwnsOne(c => c.Email)
          .HasData(
              new { CustomerDTOId = new Guid("9887b301-605f-46a6-93db-ac1ce8685723"), Address = "john.silva@gmail.com" }
          );

        modelBuilder.Entity<CustomerDTO>().OwnsOne(c => c.Phone)
          .HasData(
              new { CustomerDTOId = new Guid("9887b301-605f-46a6-93db-ac1ce8685723"), CountryCode = "55", DDD = "11", Number = "9415452222" }
          );

        modelBuilder.Entity<UserDTO>().OwnsOne(c => c.Name)
           .HasData(
               new { UserDTOId = new Guid("fa09f3a0-f22d-40a8-9cca-0c64e5ed50e4"), FullName = "John Admin" }
           );

        modelBuilder.Entity<UserDTO>().OwnsOne(c => c.Email)
         .HasData(
             new { UserDTOId = new Guid("fa09f3a0-f22d-40a8-9cca-0c64e5ed50e4"), Address = "john.admin@techfood.com" }
         );

        modelBuilder.Entity<UserDTO>()
            .HasData(
                // password: 123456
                new { Id = new Guid("fa09f3a0-f22d-40a8-9cca-0c64e5ed50e4"), Username = "john.admin", Role = "admin", PasswordHash = "AQAAAAIAAYagAAAAEKs0I0Zk5QKKieJTm20PwvTmpkSfnp5BhSl5E35ny8DqffCJA+CiDRnnKRCeOx8+mg==", IsDeleted = false }
            );

        modelBuilder.Entity<CategoryDTO>()
            .HasData(
                new { Id = new Guid("eaa76b46-2e6b-42eb-8f5d-b213f85f25ea"), SortOrder = 0, Name = "Lanche", ImageFileName = "lanche.png", IsDeleted = false },
                new { Id = new Guid("c65e2cec-bd44-446d-8ed3-a7045cd4876a"), SortOrder = 1, Name = "Acompanhamento", ImageFileName = "acompanhamento.png", IsDeleted = false },
                new { Id = new Guid("c3a70938-9e88-437d-a801-c166d2716341"), SortOrder = 2, Name = "Bebida", ImageFileName = "bebida.png", IsDeleted = false },
                new { Id = new Guid("ec2fb26d-99a4-4eab-aa5c-7dd18d88a025"), SortOrder = 3, Name = "Sobremesa", ImageFileName = "sobremesa.png", IsDeleted = false }
            );

        modelBuilder.Entity<ProductDTO>()
            .HasData(
                new { Id = new Guid("090d8eb0-f514-4248-8512-cf0d61a262f0"), Name = "X-Burguer", Description = "Delicioso X-Burguer", Price = 19.99m, CategoryDTOId = new Guid("eaa76b46-2e6b-42eb-8f5d-b213f85f25ea"), ImageFileName = "x-burguer.png", OutOfStock = false, IsDeleted = false },
                new { Id = new Guid("a62dc225-416a-4e36-ba35-a2bd2bbb80f7"), Name = "X-Salada", Description = "Delicioso X-Salada", Price = 21.99m, CategoryDTOId = new Guid("eaa76b46-2e6b-42eb-8f5d-b213f85f25ea"), ImageFileName = "x-salada.png", OutOfStock = false, IsDeleted = false },
                new { Id = new Guid("3c9374f1-58e9-4b07-bdf6-73aa2f4757ff"), Name = "X-Bacon", Description = "Delicioso X-Bacon", Price = 22.99m, CategoryDTOId = new Guid("eaa76b46-2e6b-42eb-8f5d-b213f85f25ea"), ImageFileName = "x-bacon.png", OutOfStock = false, IsDeleted = false },
                new { Id = new Guid("55f32e65-c82f-4a10-981c-cdb7b0d2715a"), Name = "Batata Frita", Description = "Crocante Batata Frita", Price = 9.99m, CategoryDTOId = new Guid("c65e2cec-bd44-446d-8ed3-a7045cd4876a"), ImageFileName = "batata.png", OutOfStock = false, IsDeleted = false },
                new { Id = new Guid("3249b4e4-11e5-41d9-9d55-e9b1d59bfb23"), Name = "Batata Frita Grande", Description = "Crocante Batata Frita", Price = 12.99m, CategoryDTOId = new Guid("c65e2cec-bd44-446d-8ed3-a7045cd4876a"), ImageFileName = "batata-grande.png", OutOfStock = false, IsDeleted = false },
                new { Id = new Guid("4aeb3ad6-1e06-418e-8878-e66a4ba9337f"), Name = "Nuggets de Frango", Description = "Delicioso Nuggets de Frango", Price = 13.99m, CategoryDTOId = new Guid("c65e2cec-bd44-446d-8ed3-a7045cd4876a"), ImageFileName = "nuggets.png", OutOfStock = false, IsDeleted = false },
                new { Id = new Guid("86c50c81-c46e-4e79-a591-3b68c75cefda"), Name = "Coca-Cola", Description = "Coca-Cola", Price = 4.99m, CategoryDTOId = new Guid("c3a70938-9e88-437d-a801-c166d2716341"), ImageFileName = "coca-cola.png", OutOfStock = false, IsDeleted = false },
                new { Id = new Guid("44c61027-8e16-444d-9f4f-e332410cccaa"), Name = "Guaraná", Description = "Guaraná", Price = 4.99m, CategoryDTOId = new Guid("c3a70938-9e88-437d-a801-c166d2716341"), ImageFileName = "guarana.png", OutOfStock = false, IsDeleted = false },
                new { Id = new Guid("bf90f247-52cc-4bbb-b6e3-9c77b6ff546f"), Name = "Fanta", Description = "Fanta", Price = 4.99m, CategoryDTOId = new Guid("c3a70938-9e88-437d-a801-c166d2716341"), ImageFileName = "fanta.png", OutOfStock = false, IsDeleted = false },
                new { Id = new Guid("8620cf54-0d37-4aa1-832a-eb98e9b36863"), Name = "Sprite", Description = "Sprite", Price = 4.99m, CategoryDTOId = new Guid("c3a70938-9e88-437d-a801-c166d2716341"), ImageFileName = "sprite.png", OutOfStock = false, IsDeleted = false },
                new { Id = new Guid("de797d9f-c473-4bed-a560-e7036ca10ab1"), Name = "Milk Shake de Morango", Description = "Milk Shake de Morango", Price = 7.99m, CategoryDTOId = new Guid("ec2fb26d-99a4-4eab-aa5c-7dd18d88a025"), ImageFileName = "milk-shake-morango.png", OutOfStock = false, IsDeleted = false },
                new { Id = new Guid("113daae6-f21f-4d38-a778-9364ac64f909"), Name = "Milk Shake de Chocolate", Description = "Milk Shake de Chocolate", Price = 7.99m, CategoryDTOId = new Guid("ec2fb26d-99a4-4eab-aa5c-7dd18d88a025"), ImageFileName = "milk-shake-chocolate.png", OutOfStock = false, IsDeleted = false },
                new { Id = new Guid("2665c2ec-c537-4d95-9a0f-791bcd4cc938"), Name = "Milk Shake de Baunilha", Description = "Milk Shake de Baunilha", Price = 7.99m, CategoryDTOId = new Guid("ec2fb26d-99a4-4eab-aa5c-7dd18d88a025"), ImageFileName = "milk-shake-baunilha.png", OutOfStock = false, IsDeleted = false }
                );

        modelBuilder.Entity<OrderDTO>()
            .HasData(
                new
                {
                    Id = new Guid("d1b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"),
                    Number = 1,
                    CustomerId = new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"),
                    CreatedAt = new DateTime(2025, 5, 13, 22, 2, 36, DateTimeKind.Utc)
            .AddTicks(6053),
                    Amount = 39.97m,
                    Status = OrderStatusTypeDTO.PreparationDone,
                    Discount = 0m,
                    IsDeleted = false
                },
                new
                {
                    Id = new Guid("f2b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"),
                    Number = 2,
                    CustomerId = new Guid("9887b301-605f-46a6-93db-ac1ce8685723"),
                    CreatedAt = new DateTime(2025, 5, 13, 22, 2, 36, DateTimeKind.Utc)
            .AddTicks(6354),
                    Amount = 26.98m,
                    Status = OrderStatusTypeDTO.InPreparation,
                    Discount = 0m,
                    IsDeleted = false
                }
            );

        modelBuilder.Entity<OrderItemDTO>()
            .HasData(
               new { Id = new Guid("ea31fb90-4bc3-418f-95fc-56516d5bc634"), OrderId = new Guid("d1b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), ProductId = new Guid("090d8eb0-f514-4248-8512-cf0d61a262f0"), Quantity = 1, UnitPrice = 19.99m, IsDeleted = false },
               new { Id = new Guid("b0f1c3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), OrderId = new Guid("d1b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), ProductId = new Guid("55f32e65-c82f-4a10-981c-cdb7b0d2715a"), Quantity = 2, UnitPrice = 9.99m, IsDeleted = false },
               new { Id = new Guid("82e5700b-c33e-40a6-bb68-7279f0509421"), OrderId = new Guid("f2b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), ProductId = new Guid("a62dc225-416a-4e36-ba35-a2bd2bbb80f7"), Quantity = 1, UnitPrice = 21.99m, IsDeleted = false },
               new { Id = new Guid("900f65fe-47ca-4b4b-9a7c-a82c6d9c52cd"), OrderId = new Guid("f2b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), ProductId = new Guid("86c50c81-c46e-4e79-a591-3b68c75cefda"), Quantity = 1, UnitPrice = 4.99m, IsDeleted = false }
            );

        //modelBuilder.Entity<PaymentType>()
        //    .HasData(
        //        new { Id = 1, Code = "MCMA", Description = "Mastercard" },
        //        new { Id = 2, Code = "VIS", Description = "Visa" },
        //        new { Id = 3, Code = "ELO", Description = "Elo" },
        //        new { Id = 4, Code = "DNR", Description = "Sodexo" },
        //        new { Id = 5, Code = "VR", Description = "Vale Refeição" },
        //        new { Id = 6, Code = "PIX", Description = "Pix" }
        //    );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
    }
}
