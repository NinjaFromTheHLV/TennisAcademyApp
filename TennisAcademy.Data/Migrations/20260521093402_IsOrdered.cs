using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsOrdered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOrdered",
                schema: "22180021",
                table: "RacketCart",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOrdered",
                schema: "22180021",
                table: "BallCart",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOrdered",
                schema: "22180021",
                table: "BagCart",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOrdered",
                schema: "22180021",
                table: "RacketCart");

            migrationBuilder.DropColumn(
                name: "IsOrdered",
                schema: "22180021",
                table: "BallCart");

            migrationBuilder.DropColumn(
                name: "IsOrdered",
                schema: "22180021",
                table: "BagCart");
        }
    }
}
