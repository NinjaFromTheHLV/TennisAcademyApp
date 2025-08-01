using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://babolat.bg/image/cache/catalog/tennis/2024/rackets/101474/101474-Pure_Drive_98-136-1-Face_2-250x250.jpg");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://i.sportisimo.com/products/images/1104/1104555/700x700/head-graphene-360-speed-mp_1.jpg");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://us.yonex.com/cdn/shop/files/EZ0898_BlastBlue_5868.jpg?v=1739481973&width=1946");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://images.squarespace-cdn.com/content/v1/56e9b38c2b8dde820241b62d/1471886555425-JT9KKFKPOL4FNLAV9ZB0/r2.jpg");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://www.tecnifibre.com/dw/image/v2/BHDN_PRD/on/demandware.static/-/Sites-tecnifibre-master-catalog/default/dwcf93310b/hi-res/T-FIGHT%202025/Packshots/305S/14FI305S5_04.jpg?sw=608&sh=608&sm=fit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
