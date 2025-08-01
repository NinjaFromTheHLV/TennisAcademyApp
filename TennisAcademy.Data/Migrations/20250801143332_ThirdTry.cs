using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ThirdTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://cdncloudcart.com/28710/products/images/134337/tenis-raketa-wilson-pro-staff-rf-97-v13-0-tns-fr-image_6358bfebb40a9_800x800.jpeg?1666760684");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "~/pictures/WilsonRacket.jpeg");
        }
    }
}
