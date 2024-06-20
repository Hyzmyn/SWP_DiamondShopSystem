using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "GemID", "ImageUrl1", "ImageUrl2", "MaterialID", "PriceRate", "ProductCode", "ProductName", "ProductionCost", "TotalPrice" },
                values: new object[,]
                {
                    { 11, 1, 1, "images/diamond_necklace_1.jpg", "images/diamond_necklace_2.jpg", 1, 2.5m, "P001", "Diamond Necklace", 150.0m, 0m },
                    { 12, 2, 2, "images/gold_ring_1.jpg", "images/gold_ring_2.jpg", 2, 2.0m, "P002", "Gold Ring", 100.0m, 0m },
                    { 13, 3, 3, "images/emerald_bracelet_1.jpg", "images/emerald_bracelet_2.jpg", 3, 2.2m, "P003", "Emerald Bracelet", 120.0m, 0m },
                    { 14, 4, 4, "images/silver_earrings_1.jpg", "images/silver_earrings_2.jpg", 4, 1.8m, "P004", "Silver Earrings", 80.0m, 0m },
                    { 15, 3, 1, "images/sapphire_pendant_1.jpg", "images/sapphire_pendant_2.jpg", 2, 2.3m, "P005", "Sapphire Pendant", 130.0m, 0m },
                    { 16, 4, 2, "images/platinum_bracelet_1.jpg", "images/platinum_bracelet_2.jpg", 3, 2.7m, "P006", "Platinum Bracelet", 200.0m, 0m },
                    { 17, 1, 3, "images/ruby_ring_1.jpg", "images/ruby_ring_2.jpg", 4, 2.1m, "P007", "Ruby Ring", 90.0m, 0m },
                    { 18, 2, 4, "images/amethyst_earrings_1.jpg", "images/amethyst_earrings_2.jpg", 1, 1.9m, "P008", "Amethyst Earrings", 70.0m, 0m },
                    { 19, 1, 1, "images/topaz_necklace_1.jpg", "images/topaz_necklace_2.jpg", 3, 2.4m, "P009", "Topaz Necklace", 110.0m, 0m },
                    { 20, 2, 2, "images/opal_brooch_1.jpg", "images/opal_brooch_2.jpg", 4, 2.0m, "P010", "Opal Brooch", 95.0m, 0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 20);
        }
    }
}
