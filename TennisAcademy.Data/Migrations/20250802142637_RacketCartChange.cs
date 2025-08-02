using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RacketCartChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RacketCart",
                table: "RacketCart");

            migrationBuilder.DropIndex(
                name: "IX_RacketCart_RacketId",
                table: "RacketCart");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RacketCart");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RacketCart",
                table: "RacketCart",
                columns: new[] { "RacketId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RacketCart",
                table: "RacketCart");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RacketCart",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Racket Cart Identifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RacketCart",
                table: "RacketCart",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RacketCart_RacketId",
                table: "RacketCart",
                column: "RacketId");
        }
    }
}
