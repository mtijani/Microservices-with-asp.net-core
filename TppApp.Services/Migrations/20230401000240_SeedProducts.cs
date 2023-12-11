using Microsoft.EntityFrameworkCore.Migrations;

namespace TppApp.Services.ProductAPI.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Appetizer", " Delicious and delightful  .<br/>Spicy Tunisian Chorba", "https://dotnettp.blob.core.windows.net/food/Chorba.jpg", "Chorba", 15.0 },
                    { 2, "Appetizer", "North african Tajine with eggs, cheese and meat ", "https://dotnettp.blob.core.windows.net/food/tajine.jpg", "Tajine", 13.99 },
                    { 3, "Dessert", "authentic baklawa", "https://dotnettp.blob.core.windows.net/food/Baklawa-Tunisienne.jpg", "Baklava", 10.99 },
                    { 4, "Entree", "Delicious kouskous with vegetables and fish", "https://dotnettp.blob.core.windows.net/food/couscous-tunisien.jpeg", "Kouskous", 15.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);
        }
    }
}
