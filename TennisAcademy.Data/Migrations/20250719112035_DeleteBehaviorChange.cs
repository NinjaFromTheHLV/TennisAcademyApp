using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBehaviorChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Coaches_CoachId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersCoaches_AspNetUsers_UserId",
                table: "UsersCoaches");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersCoaches_Coaches_CoachId",
                table: "UsersCoaches");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f94fb93-99f7-4701-b49e-c668e467080e", "AQAAAAIAAYagAAAAEN9sk38YaAN7umTNwFb9GdxL9eYs6bSrKnVJb95jVgMjS8K8NbBdm1DZhScUtVbpAw==", "3fbc05fd-4f73-425d-9db8-0c7bd7721847" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Coaches_CoachId",
                table: "Reservations",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCoaches_AspNetUsers_UserId",
                table: "UsersCoaches",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCoaches_Coaches_CoachId",
                table: "UsersCoaches",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "CoachId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Coaches_CoachId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersCoaches_AspNetUsers_UserId",
                table: "UsersCoaches");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersCoaches_Coaches_CoachId",
                table: "UsersCoaches");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a06c90e-f511-42b1-b75d-59792c8de60b", "AQAAAAIAAYagAAAAEEjKhzFuu9hTfvjeH9dVBfbVUIy9lv8O0MurgGxE9d/m/F5N2R8fgPQWQElZLhQIdw==", "21d083be-fbfd-4780-8a08-c6dda98350c5" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Coaches_CoachId",
                table: "Reservations",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "CoachId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCoaches_AspNetUsers_UserId",
                table: "UsersCoaches",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCoaches_Coaches_CoachId",
                table: "UsersCoaches",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "CoachId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
