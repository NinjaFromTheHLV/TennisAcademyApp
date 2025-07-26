using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReservationIsDeletedPropertyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0019d85-6fee-44fd-90f4-29cbf0a1d9b6", "AQAAAAIAAYagAAAAEI/hgwo6J5L36Pt6BpMSOXnJ5kaAMIZKuGfH/TWCvvdkp8XAK0jUsj/Cp9EAHMLxuQ==", "f345da46-0692-4d77-afe1-7dddd94a9896" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reservations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e909aa46-df31-4dad-8863-216361afcfd9", "AQAAAAIAAYagAAAAENSPTocZsWupEqiI5gVLElxy5Hv5vYLdHTtw4Oy0I8rr8LlxQuDU7owCIPqV9v56LQ==", "e64463ec-2510-4265-a911-c60cf20ee29a" });
        }
    }
}
