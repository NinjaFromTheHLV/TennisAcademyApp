using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RacketsImageUrlConfigured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/rackets/wilson_prostaff.jpg");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/rackets/babolat_puredrive.jpg");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/rackets/head_speed.jpg");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "/images/rackets/yonex_ezone98.jpg");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "/images/rackets/prince_tour100p.jpg");

            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "/images/rackets/tecnifibre_tfight305.jpg");
        }
    }
}
