using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RacketsImageUrlSecondTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "~/pictures/WilsonRacket.jpeg");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "~/pictures/BabolatRacket.jpg");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "~/pictures/HeadRacket.jpg");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "~/pictures/YonexRacket.webp");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "~/pictures/PrinceTourRacket.jpg");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "~/pictures/TecnifibreRacket.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "~/pictures/WilsonRacket");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "~/pictures/BabolatRacket");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "~/pictures/HeadRacket");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "~/pictures/YonexRacket");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "~/pictures/PrinceTourRacket");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "~/pictures/TecnifibreRacket");
        }
    }
}
