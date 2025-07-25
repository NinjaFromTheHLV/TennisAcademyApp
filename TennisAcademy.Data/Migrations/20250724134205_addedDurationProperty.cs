using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedDurationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Duration of the session");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba41741c-10c8-470f-8f7c-8ad7d1cfc297", "AQAAAAIAAYagAAAAEAIhc/AjxYkB6HbNbNZAIQJbwgqnDbMZPNJlTMfqcDm4xf0o2eVNe4Nvts3E8IfOaA==", "51260f59-16e2-4821-b172-373e58dbdcb6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Reservations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9782f2ee-eae0-4714-b0ec-22cbc141ebab", "AQAAAAIAAYagAAAAEE3oijHanhHMuvxTgbS3Tn9e82dmmQcazmnlXSsZfePsw4JMHVlCm01z/2SEDArQtw==", "499509da-e67f-490d-9127-d691737c951a" });
        }
    }
}
