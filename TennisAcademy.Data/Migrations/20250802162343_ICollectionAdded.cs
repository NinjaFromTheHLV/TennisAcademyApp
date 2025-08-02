using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ICollectionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BallId",
                table: "RacketCart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RacketCart_BallId",
                table: "RacketCart",
                column: "BallId");

            migrationBuilder.AddForeignKey(
                name: "FK_RacketCart_Balls_BallId",
                table: "RacketCart",
                column: "BallId",
                principalTable: "Balls",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RacketCart_Balls_BallId",
                table: "RacketCart");

            migrationBuilder.DropIndex(
                name: "IX_RacketCart_BallId",
                table: "RacketCart");

            migrationBuilder.DropColumn(
                name: "BallId",
                table: "RacketCart");
        }
    }
}
