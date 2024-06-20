using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gems",
                columns: table => new
                {
                    GemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FourC = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Proportion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Polish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symmetry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fluorescence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gems", x => x.GemID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "GemPriceLists",
                columns: table => new
                {
                    GemID = table.Column<int>(type: "int", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaratWeight = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clarity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    EffDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GemPriceLists", x => x.GemID);
                    table.ForeignKey(
                        name: "FK_GemPriceLists_Gems_GemID",
                        column: x => x.GemID,
                        principalTable: "Gems",
                        principalColumn: "GemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GemID = table.Column<int>(type: "int", nullable: false),
                    MaterialID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    ProductionCost = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    PriceRate = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "ProductCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
<<<<<<<< HEAD:Repository/Migrations/20240619200445_v1.cs
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    TimeOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatus = table.Column<bool>(type: "bit", nullable: false)
========
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    UserStatus = table.Column<bool>(type: "bit", nullable: false),
                    NiSize = table.Column<string>(type: "nvarchar(max)", nullable: false)
>>>>>>>> 0dcabfb594cab0d49ac7c8ac67f60a3b32440c83:Repository/Migrations/20240617083611_InitialCreate.cs
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductGems",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    GemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGems", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_ProductGems_Gems_GemID",
                        column: x => x.GemID,
                        principalTable: "Gems",
                        principalColumn: "GemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductGems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMaterials",
                columns: table => new
                {
                    MaterialID = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaterials", x => x.MaterialID);
                    table.ForeignKey(
                        name: "FK_ProductMaterials_Products_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TimeOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialPriceLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialID = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SellPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialPriceLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialPriceLists_ProductMaterials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "ProductMaterials",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    DiscountCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    DiscountDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountID);
                    table.ForeignKey(
                        name: "FK_Discounts_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    NiSize = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warranties",
                columns: table => new
                {
                    WarrantyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    BuyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Instance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarrantyStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warranties", x => x.WarrantyID);
                    table.ForeignKey(
                        name: "FK_Warranties_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warranties_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleName" },
                values: new object[,]
                {
<<<<<<<< HEAD:Repository/Migrations/20240619200445_v1.cs
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialID = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    SellPrice = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    EffDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialPriceLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialPriceLists_ProductMaterials_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "ProductMaterials",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Cascade);
========
                    { 1, "Customer" },
                    { 2, "SalesStaff" },
                    { 3, "Manager" },
                    { 4, "Admin" }
>>>>>>>> 0dcabfb594cab0d49ac7c8ac67f60a3b32440c83:Repository/Migrations/20240617083611_InitialCreate.cs
                });

            migrationBuilder.InsertData(
                table: "Gems",
                columns: new[] { "GemID", "Active", "Fluorescence", "FourC", "GemCode", "GemName", "Origin", "Polish", "Proportion", "Symmetry" },
                values: new object[,]
                {
                    { 1, true, "None", 4.5m, "EMGR-001", "Emerald", "Brazil", "Excellent", "Excellent", "Excellent" },
                    { 2, true, "Faint", 3.8m, "SAPP-002", "Sapphire", "Australia", "Excellent", "Very Good", "Very Good" },
                    { 3, true, "None", 4.9m, "DIAM-003", "Diamond", "Canada", "Ideal", "Ideal", "Ideal" },
                    { 4, true, "Faint", 3.2m, "RUBY-004", "Ruby", "Russia", "Very Good", "Excellent", "Excellent" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryID", "CategoryName", "CategoryStatus", "CategoryType" },
                values: new object[,]
                {
                    { 1, "Rings", true, "Jewelry" },
                    { 2, "Necklaces", true, "Jewelry" },
                    { 3, "Bracelets", true, "Jewelry" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "Email", "NiSize", "Password", "PhoneNumber", "RoleID", "UserStatus", "Username" },
                values: new object[,]
                {
                    { 1, "Address1", "user1@example.com", "S", "123", "1234567890", 1, true, "User1" },
                    { 2, "Address2", "user2@example.com", "M", "123", "0987654321", 2, true, "User2" },
                    { 3, "Address3", "user3@example.com", "M", "123", "0987654321", 3, true, "User3" },
                    { 4, "Address4", "user4@example.com", "M", "123", "0987654321", 4, true, "User4" },
                    { 5, "Address5", "user5@example.com", "M", "123", "0987654321", 5, true, "User5" },
                    { 6, "Address", "user6@example.com", "M", "Password", "0987654321", 5, true, "User6" },
                    { 7, "Address", "user7@example.com", "M", "Password", "0987654321", 5, true, "User7" },
                    { 8, "Address", "user8@example.com", "M", "Password", "0987654321", 5, true, "User8" },
                    { 9, "Address", "user9@example.com", "M", "Password", "0987654321", 5, true, "User9" }
                });

            migrationBuilder.InsertData(
                table: "GemPriceLists",
                columns: new[] { "GemID", "CaratWeight", "Clarity", "Color", "Cut", "EffDate", "Origin", "Price" },
                values: new object[,]
                {
                    { 1, 2.05m, "VVS1", "D", "Brilliant", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brazil", 10000m },
                    { 2, 1.8m, "VS2", "E", "Cushion", new DateTime(2023, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Australia", 8500m },
                    { 3, 3.02m, "IF", "F", "Round Brilliant", new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Canada", 25000m },
                    { 4, 1.2m, "SI1", "J", "Oval", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Russia", 6000m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "Note", "OrderStatus", "TimeOrder", "TotalPrice", "UserID" },
                values: new object[,]
                {
                    { 1, "Express delivery", true, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 175.0m, 1 },
                    { 2, "Standard delivery", true, new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 170.0m, 2 },
                    { 3, "Pickup in-store", false, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 120.0m, 3 },
                    { 4, "Express delivery", true, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 90.0m, 1 },
                    { 5, "Standard delivery", true, new DateTime(2023, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.0m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "GemID", "ImageUrl1", "ImageUrl2", "MaterialID", "PriceRate", "ProductCode", "ProductName", "ProductionCost" },
                values: new object[,]
                {
                    { 1, 1, 1, "", "", 1, 2.5m, "P001", "Diamond Necklace", 150.0m },
                    { 2, 2, 2, "", "", 2, 2.0m, "P002", "Gold Ring", 100.0m },
                    { 3, 3, 3, "", "", 3, 2.2m, "P003", "Emerald Bracelet", 120.0m },
                    { 4, 3, 4, "", "", 4, 1.8m, "P004", "Silver Earrings", 80.0m }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "DiscountID", "DiscountAmount", "DiscountCode", "DiscountDate", "DiscountStatus", "OrderID" },
                values: new object[,]
                {
                    { 1, 10.0m, "SUMMERSALE10", new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1 },
                    { 2, 15.0m, "NEWCUSTOMER15", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2 },
                    { 3, 20.0m, "HOLIDAYDISCOUNT20", new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3 },
                    { 4, 5.0m, "SPECIALOFFER5", new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailID", "NiSize", "OrderID", "Price", "ProductID", "Quantity" },
                values: new object[,]
                {
                    { 1, "7", 1, 50.0m, 1, 2 },
                    { 2, "8", 1, 75.0m, 2, 1 },
                    { 3, "6", 2, 40.0m, 3, 3 },
                    { 4, "9", 2, 90.0m, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductGems",
                columns: new[] { "ProductID", "GemID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductMaterials",
                columns: new[] { "MaterialID", "Weight" },
                values: new object[,]
                {
                    { 1, 20.5m },
                    { 2, 15.7m },
                    { 3, 18.2m },
                    { 4, 22.1m }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "WarrantyID", "BuyDate", "EndDate", "Instance", "OrderID", "ProductID", "WarrantyStatus" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ACME-01", 1, 1, true },
                    { 2, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TECH-02", 2, 2, true },
                    { 3, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ELITE-03", 3, 3, true },
                    { 4, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "BASIC-04", 4, 4, true }
                });

            migrationBuilder.InsertData(
                table: "MaterialPriceLists",
                columns: new[] { "ID", "BuyPrice", "EffDate", "MaterialID", "SellPrice" },
                values: new object[,]
                {
                    { 1, 18.75m, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 25.0m },
                    { 2, 14.50m, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 20.0m },
                    { 3, 16.25m, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 22.0m },
                    { 4, 19.00m, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 26.0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_OrderID",
                table: "Discounts",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialPriceLists_MaterialID",
                table: "MaterialPriceLists",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductID",
                table: "OrderDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGems_GemID",
                table: "ProductGems",
                column: "GemID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_OrderID",
                table: "Warranties",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_ProductID",
                table: "Warranties",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "GemPriceLists");

            migrationBuilder.DropTable(
                name: "MaterialPriceLists");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductGems");

            migrationBuilder.DropTable(
                name: "Warranties");

            migrationBuilder.DropTable(
                name: "ProductMaterials");

            migrationBuilder.DropTable(
                name: "Gems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
