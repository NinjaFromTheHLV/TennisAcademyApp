using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SurfacesImageUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://www.tennisnerd.net/wp-content/uploads/2024/06/grass-tennis.webp");

            migrationBuilder.UpdateData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://asltenniscourts.com.au/wp-content/uploads/2021/03/AdobeStock_253105355-1024x683.jpeg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://asltenniscourts.com.au/wp-content/uploads/2021/03/AdobeStock_253105355-1024x683.jpeg");

            migrationBuilder.UpdateData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://www.tennisnerd.net/wp-content/uploads/2024/06/grass-tennis.webp");
        }
    }
}
