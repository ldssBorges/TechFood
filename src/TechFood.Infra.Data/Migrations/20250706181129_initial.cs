using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechFood.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    ImageFileName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameFullName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    DocumentValue = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    PhoneCountryCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    PhoneDDD = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Preparation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preparation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameFullName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "varchar(50)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OutOfStock = table.Column<bool>(type: "bit", nullable: false),
                    ImageFileName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistory_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductDTOId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductDTOId",
                        column: x => x.ProductDTOId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "ImageFileName", "IsDeleted", "Name", "SortOrder" },
                values: new object[,]
                {
                    { new Guid("c3a70938-9e88-437d-a801-c166d2716341"), "bebida.png", false, "Bebida", 2 },
                    { new Guid("c65e2cec-bd44-446d-8ed3-a7045cd4876a"), "acompanhamento.png", false, "Acompanhamento", 1 },
                    { new Guid("eaa76b46-2e6b-42eb-8f5d-b213f85f25ea"), "lanche.png", false, "Lanche", 0 },
                    { new Guid("ec2fb26d-99a4-4eab-aa5c-7dd18d88a025"), "sobremesa.png", false, "Sobremesa", 3 }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "IsDeleted", "EmailAddress", "NameFullName", "DocumentType", "DocumentValue", "PhoneCountryCode", "PhoneDDD", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"), false, "john.dev@gmail.com", "John", 0, "4511554544", "55", "11", "9415452222" },
                    { new Guid("9887b301-605f-46a6-93db-ac1ce8685723"), false, "john.silva@gmail.com", "John Silva", 0, "000000000191", "55", "11", "9415452222" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "IsDeleted", "PasswordHash", "Role", "Username", "EmailAddress", "NameFullName" },
                values: new object[] { new Guid("fa09f3a0-f22d-40a8-9cca-0c64e5ed50e4"), false, "AQAAAAIAAYagAAAAEKs0I0Zk5QKKieJTm20PwvTmpkSfnp5BhSl5E35ny8DqffCJA+CiDRnnKRCeOx8+mg==", "admin", "john.admin", "john.admin@techfood.com", "John Admin" });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "Amount", "CreatedAt", "CustomerId", "Discount", "FinishedAt", "IsDeleted", "Status" },
                values: new object[,]
                {
                    { new Guid("d1b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), 39.97m, new DateTime(2025, 5, 13, 22, 2, 36, 0, DateTimeKind.Utc).AddTicks(6053), new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"), 0m, null, false, 5 },
                    { new Guid("f2b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), 26.98m, new DateTime(2025, 5, 13, 22, 2, 36, 0, DateTimeKind.Utc).AddTicks(6354), new Guid("9887b301-605f-46a6-93db-ac1ce8685723"), 0m, null, false, 4 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "ImageFileName", "IsDeleted", "Name", "OutOfStock", "Price" },
                values: new object[,]
                {
                    { new Guid("090d8eb0-f514-4248-8512-cf0d61a262f0"), new Guid("eaa76b46-2e6b-42eb-8f5d-b213f85f25ea"), "Delicioso X-Burguer", "x-burguer.png", false, "X-Burguer", false, 19.99m },
                    { new Guid("113daae6-f21f-4d38-a778-9364ac64f909"), new Guid("ec2fb26d-99a4-4eab-aa5c-7dd18d88a025"), "Milk Shake de Chocolate", "milk-shake-chocolate.png", false, "Milk Shake de Chocolate", false, 7.99m },
                    { new Guid("2665c2ec-c537-4d95-9a0f-791bcd4cc938"), new Guid("ec2fb26d-99a4-4eab-aa5c-7dd18d88a025"), "Milk Shake de Baunilha", "milk-shake-baunilha.png", false, "Milk Shake de Baunilha", false, 7.99m },
                    { new Guid("3249b4e4-11e5-41d9-9d55-e9b1d59bfb23"), new Guid("c65e2cec-bd44-446d-8ed3-a7045cd4876a"), "Crocante Batata Frita", "batata-grande.png", false, "Batata Frita Grande", false, 12.99m },
                    { new Guid("3c9374f1-58e9-4b07-bdf6-73aa2f4757ff"), new Guid("eaa76b46-2e6b-42eb-8f5d-b213f85f25ea"), "Delicioso X-Bacon", "x-bacon.png", false, "X-Bacon", false, 22.99m },
                    { new Guid("44c61027-8e16-444d-9f4f-e332410cccaa"), new Guid("c3a70938-9e88-437d-a801-c166d2716341"), "Guaraná", "guarana.png", false, "Guaraná", false, 4.99m },
                    { new Guid("4aeb3ad6-1e06-418e-8878-e66a4ba9337f"), new Guid("c65e2cec-bd44-446d-8ed3-a7045cd4876a"), "Delicioso Nuggets de Frango", "nuggets.png", false, "Nuggets de Frango", false, 13.99m },
                    { new Guid("55f32e65-c82f-4a10-981c-cdb7b0d2715a"), new Guid("c65e2cec-bd44-446d-8ed3-a7045cd4876a"), "Crocante Batata Frita", "batata.png", false, "Batata Frita", false, 9.99m },
                    { new Guid("8620cf54-0d37-4aa1-832a-eb98e9b36863"), new Guid("c3a70938-9e88-437d-a801-c166d2716341"), "Sprite", "sprite.png", false, "Sprite", false, 4.99m },
                    { new Guid("86c50c81-c46e-4e79-a591-3b68c75cefda"), new Guid("c3a70938-9e88-437d-a801-c166d2716341"), "Coca-Cola", "coca-cola.png", false, "Coca-Cola", false, 4.99m },
                    { new Guid("a62dc225-416a-4e36-ba35-a2bd2bbb80f7"), new Guid("eaa76b46-2e6b-42eb-8f5d-b213f85f25ea"), "Delicioso X-Salada", "x-salada.png", false, "X-Salada", false, 21.99m },
                    { new Guid("bf90f247-52cc-4bbb-b6e3-9c77b6ff546f"), new Guid("c3a70938-9e88-437d-a801-c166d2716341"), "Fanta", "fanta.png", false, "Fanta", false, 4.99m },
                    { new Guid("de797d9f-c473-4bed-a560-e7036ca10ab1"), new Guid("ec2fb26d-99a4-4eab-aa5c-7dd18d88a025"), "Milk Shake de Morango", "milk-shake-morango.png", false, "Milk Shake de Morango", false, 7.99m }
                });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "IsDeleted", "OrderId", "ProductDTOId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("82e5700b-c33e-40a6-bb68-7279f0509421"), false, new Guid("f2b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), null, new Guid("a62dc225-416a-4e36-ba35-a2bd2bbb80f7"), 1, 21.99m },
                    { new Guid("900f65fe-47ca-4b4b-9a7c-a82c6d9c52cd"), false, new Guid("f2b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), null, new Guid("86c50c81-c46e-4e79-a591-3b68c75cefda"), 1, 4.99m },
                    { new Guid("b0f1c3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), false, new Guid("d1b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), null, new Guid("55f32e65-c82f-4a10-981c-cdb7b0d2715a"), 2, 9.99m },
                    { new Guid("ea31fb90-4bc3-418f-95fc-56516d5bc634"), false, new Guid("d1b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), null, new Guid("090d8eb0-f514-4248-8512-cf0d61a262f0"), 1, 19.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_OrderId",
                table: "OrderHistory",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductDTOId",
                table: "OrderItem",
                column: "ProductDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderHistory");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Preparation");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
