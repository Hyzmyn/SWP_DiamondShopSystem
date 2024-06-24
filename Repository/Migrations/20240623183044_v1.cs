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
                name: "PriceRateLists",
                columns: table => new
                {
                    PriceRateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceRate = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    EffDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRateLists", x => x.PriceRateID);
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
                    PriceRateID = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    UserStatus = table.Column<bool>(type: "bit", nullable: false),
                    NiSize = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
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
                name: "PriceRateListProduct",
                columns: table => new
                {
                    PriceRateID = table.Column<int>(type: "int", nullable: false),
                    PriceRateListsPriceRateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRateListProduct", x => new { x.PriceRateID, x.PriceRateListsPriceRateID });
                    table.ForeignKey(
                        name: "FK_PriceRateListProduct_PriceRateLists_PriceRateListsPriceRateID",
                        column: x => x.PriceRateListsPriceRateID,
                        principalTable: "PriceRateLists",
                        principalColumn: "PriceRateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceRateListProduct_Products_PriceRateID",
                        column: x => x.PriceRateID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
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
                    TotalPrice = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
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
                table: "PriceRateLists",
                columns: new[] { "PriceRateID", "EffDate", "PriceRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m },
                    { 2, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m },
                    { 3, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 11m },
                    { 4, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 11m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "GemID", "ImageUrl1", "ImageUrl2", "MaterialID", "PriceRateID", "ProductCode", "ProductName", "ProductionCost", "TotalCost" },
                values: new object[,]
                {
                    { 1, 1, 1, "images/diamond_necklace_1.jpg", "images/diamond_necklace_2.jpg", 1, 1m, "P001", "Diamond Necklace", 150.0m, 0m },
                    { 2, 2, 2, "images/gold_ring_1.jpg", "images/gold_ring_2.jpg", 2, 1m, "P002", "Gold Ring", 100.0m, 0m },
                    { 3, 3, 3, "images/emerald_bracelet_1.jpg", "images/emerald_bracelet_2.jpg", 3, 1m, "P003", "Emerald Bracelet", 120.0m, 0m },
                    { 4, 4, 4, "images/silver_earrings_1.jpg", "images/silver_earrings_2.jpg", 4, 1m, "P004", "Silver Earrings", 80.0m, 0m },
                    { 5, 3, 1, "images/sapphire_pendant_1.jpg", "images/sapphire_pendant_2.jpg", 2, 1m, "P005", "Sapphire Pendant", 130.0m, 0m },
                    { 6, 4, 2, "images/platinum_bracelet_1.jpg", "images/platinum_bracelet_2.jpg", 3, 1m, "P006", "Platinum Bracelet", 200.0m, 0m },
                    { 7, 1, 3, "images/ruby_ring_1.jpg", "images/ruby_ring_2.jpg", 4, 1m, "P007", "Ruby Ring", 90.0m, 0m },
                    { 8, 2, 4, "images/amethyst_earrings_1.jpg", "images/amethyst_earrings_2.jpg", 1, 1m, "P008", "Amethyst Earrings", 70.0m, 0m },
                    { 9, 1, 1, "images/topaz_necklace_1.jpg", "images/topaz_necklace_2.jpg", 3, 1m, "P009", "Topaz Necklace", 110.0m, 0m },
                    { 10, 2, 2, "images/opal_brooch_1.jpg", "images/opal_brooch_2.jpg", 4, 1m, "P010", "Opal Brooch", 95.0m, 0m },
                    { 11, 1, 1, "images/diamond_necklace_1.jpg", "images/diamond_necklace_2.jpg", 1, 1m, "P001", "Diamond Necklace", 150.0m, 0m },
                    { 12, 2, 2, "images/gold_ring_1.jpg", "images/gold_ring_2.jpg", 2, 1m, "P002", "Gold Ring", 100.0m, 0m },
                    { 13, 3, 3, "images/emerald_bracelet_1.jpg", "images/emerald_bracelet_2.jpg", 3, 1m, "P003", "Emerald Bracelet", 120.0m, 0m },
                    { 14, 4, 4, "images/silver_earrings_1.jpg", "images/silver_earrings_2.jpg", 4, 1m, "P004", "Silver Earrings", 80.0m, 0m },
                    { 15, 3, 1, "images/sapphire_pendant_1.jpg", "images/sapphire_pendant_2.jpg", 2, 1m, "P005", "Sapphire Pendant", 130.0m, 0m },
                    { 16, 4, 2, "images/platinum_bracelet_1.jpg", "images/platinum_bracelet_2.jpg", 3, 1m, "P006", "Platinum Bracelet", 200.0m, 0m },
                    { 17, 1, 3, "images/ruby_ring_1.jpg", "images/ruby_ring_2.jpg", 4, 1m, "P007", "Ruby Ring", 90.0m, 0m },
                    { 18, 2, 4, "images/amethyst_earrings_1.jpg", "images/amethyst_earrings_2.jpg", 1, 1m, "P008", "Amethyst Earrings", 70.0m, 0m },
                    { 19, 1, 1, "images/topaz_necklace_1.jpg", "images/topaz_necklace_2.jpg", 3, 1m, "P009", "Topaz Necklace", 110.0m, 0m },
                    { 20, 2, 2, "images/opal_brooch_1.jpg", "images/opal_brooch_2.jpg", 4, 1m, "P010", "Opal Brooch", 95.0m, 0m }
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
                table: "MaterialPriceLists",
                columns: new[] { "ID", "BuyPrice", "EffDate", "MaterialID", "SellPrice" },
                values: new object[,]
                {
                    { 1, 18.75m, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 25.0m },
                    { 2, 14.50m, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 20.0m },
                    { 3, 16.25m, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 22.0m },
                    { 4, 19.00m, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 26.0m }
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
                table: "Warranties",
                columns: new[] { "WarrantyID", "BuyDate", "EndDate", "Instance", "OrderID", "ProductID", "WarrantyStatus" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ACME-01", 1, 1, true },
                    { 2, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TECH-02", 2, 2, true },
                    { 3, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ELITE-03", 3, 3, true },
                    { 4, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "BASIC-04", 4, 4, true }
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
                name: "IX_PriceRateListProduct_PriceRateListsPriceRateID",
                table: "PriceRateListProduct",
                column: "PriceRateListsPriceRateID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGems_GemID",
                table: "ProductGems",
                column: "GemID");

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
                name: "PriceRateListProduct");

            migrationBuilder.DropTable(
                name: "ProductGems");

            migrationBuilder.DropTable(
                name: "Warranties");

            migrationBuilder.DropTable(
                name: "ProductMaterials");

            migrationBuilder.DropTable(
                name: "PriceRateLists");

            migrationBuilder.DropTable(
                name: "Gems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
