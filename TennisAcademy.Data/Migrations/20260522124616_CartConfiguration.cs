using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CartConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RacketCart",
                schema: "22180021",
                table: "RacketCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BallCart",
                schema: "22180021",
                table: "BallCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BagCart",
                schema: "22180021",
                table: "BagCart");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RacketCart",
                schema: "22180021",
                table: "RacketCart",
                columns: new[] { "RacketId", "UserId", "IsGift" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BallCart",
                schema: "22180021",
                table: "BallCart",
                columns: new[] { "BallId", "UserId", "IsGift" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BagCart",
                schema: "22180021",
                table: "BagCart",
                columns: new[] { "BagId", "UserId", "IsGift" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RacketCart",
                schema: "22180021",
                table: "RacketCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BallCart",
                schema: "22180021",
                table: "BallCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BagCart",
                schema: "22180021",
                table: "BagCart");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RacketCart",
                schema: "22180021",
                table: "RacketCart",
                columns: new[] { "RacketId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BallCart",
                schema: "22180021",
                table: "BallCart",
                columns: new[] { "BallId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BagCart",
                schema: "22180021",
                table: "BagCart",
                columns: new[] { "BagId", "UserId" });
        }
    }
}
