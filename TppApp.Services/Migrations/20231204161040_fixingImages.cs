using Microsoft.EntityFrameworkCore.Migrations;

namespace TppApp.Services.ProductAPI.Migrations
{
    public partial class fixingImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "authentic baklava", "Baklawa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "authentic baklawa", "Baklava" });
        }
    }
}
