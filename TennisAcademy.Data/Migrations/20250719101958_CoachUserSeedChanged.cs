using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CoachUserSeedChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a06c90e-f511-42b1-b75d-59792c8de60b", "AQAAAAIAAYagAAAAEEjKhzFuu9hTfvjeH9dVBfbVUIy9lv8O0MurgGxE9d/m/F5N2R8fgPQWQElZLhQIdw==", "21d083be-fbfd-4780-8a08-c6dda98350c5" });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 1,
                column: "UserId",
                value: "5542dacf-f728-49be-8594-2100c4bfd5c8");

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 2,
                column: "UserId",
                value: "5542dacf-f728-49be-8594-2100c4bfd5c8");

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 3,
                column: "UserId",
                value: "5542dacf-f728-49be-8594-2100c4bfd5c8");

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 4,
                column: "UserId",
                value: "5542dacf-f728-49be-8594-2100c4bfd5c8");

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 5,
                column: "UserId",
                value: "5542dacf-f728-49be-8594-2100c4bfd5c8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3ff9796-1f86-4286-a8e3-8dbea92a9e20", "AQAAAAIAAYagAAAAEL/jotCs12eXoD9Ne5YvD0oKavfgcVzJKECZy0lN68IGy5Ojd5gajc2kwQ4YDcBuJA==", "e5bdfc7a-c763-4e0f-8f3b-9860cea9f650" });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 1,
                column: "UserId",
                value: "seed-user-id-123");

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 2,
                column: "UserId",
                value: "seed-user-id-123");

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 3,
                column: "UserId",
                value: "seed-user-id-123");

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 4,
                column: "UserId",
                value: "seed-user-id-123");

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 5,
                column: "UserId",
                value: "seed-user-id-123");
        }
    }
}
