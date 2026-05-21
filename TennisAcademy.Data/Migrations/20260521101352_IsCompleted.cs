using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                schema: "22180021",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                schema: "22180021",
                table: "Reservations");
        }
    }
}
