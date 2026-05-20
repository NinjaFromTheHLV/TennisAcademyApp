using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CoachRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "22180021",
                table: "Coaches",
                type: "nvarchar(450)",
                nullable: true,
                comment: "Identity User Identifier linked to this coach");

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 3,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 4,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 5,
                column: "UserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_UserId",
                schema: "22180021",
                table: "Coaches",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coaches_AspNetUsers_UserId",
                schema: "22180021",
                table: "Coaches",
                column: "UserId",
                principalSchema: "22180021",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coaches_AspNetUsers_UserId",
                schema: "22180021",
                table: "Coaches");

            migrationBuilder.DropIndex(
                name: "IX_Coaches_UserId",
                schema: "22180021",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "22180021",
                table: "Coaches");
        }
    }
}
