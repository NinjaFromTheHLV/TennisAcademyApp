using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class BagSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bags",
                columns: new[] { "Id", "Brand", "ImageUrl", "Model", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Wilson", "https://cdn.media.amplience.net/i/sportinglife/25918789_0/Team-3-Pack-Tennis-Bag?$default$&fmt=auto&w=540&h=540", "Team 3-Pack", 59.99m, 10 },
                    { 2, "Head", "https://media.strefatenisa.com.pl/public/media/20/c1/2b/1721072068/head-tour-team-6r-combi-black-mixed-1.jpg?ts=1745860751", "Tour Team 6R", 89.99m, 7 },
                    { 3, "Babolat", "https://m.media-amazon.com/images/I/61vGrieRbCL._UF1000,1000_QL80_.jpg", "Pure Drive RHx6", 99.99m, 5 },
                    { 4, "Yonex", "https://www.midwestracquetsports.com/images/xl/BAG92429BK.jpg?v=1", "Pro Series 9-Pack", 129.99m, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bags",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
