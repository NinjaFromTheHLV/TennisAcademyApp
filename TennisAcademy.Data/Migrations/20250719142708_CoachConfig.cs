using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CoachConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coaches",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Coach Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Coach Name");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Coaches",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                comment: "Coach Description",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Coach Description");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9782f2ee-eae0-4714-b0ec-22cbc141ebab", "AQAAAAIAAYagAAAAEE3oijHanhHMuvxTgbS3Tn9e82dmmQcazmnlXSsZfePsw4JMHVlCm01z/2SEDArQtw==", "499509da-e67f-490d-9127-d691737c951a" });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "CoachId", "Age", "Description", "ImageUrl", "Name", "Nationality", "UserId" },
                values: new object[,]
                {
                    { 1, 38, "One of the greatest tennis players of all time, known for his clay court dominance.", "~/pictures/rafa.jpg", "Rafael Nadal", "Spanish", "90222459-c5ca-436e-a8b3-b92e0669c683" },
                    { 2, 43, "Swiss tennis legend with unmatched elegance and 20 Grand Slam titles.", "https://a.espncdn.com/combiner/i?img=/i/headshots/tennis/players/full/425.png", "Roger Federer", "Swiss", "90222459-c5ca-436e-a8b3-b92e0669c683" },
                    { 3, 37, "Serbian champion, known for his resilience and complete game.", "https://a.espncdn.com/i/headshots/tennis/players/full/296.png", "Novak Djokovic", "Serbian", "90222459-c5ca-436e-a8b3-b92e0669c683" },
                    { 4, 55, "American icon who redefined tennis in the 90s with a colorful personality.", "https://www.atptour.com/-/media/alias/player-headshot/A092", "Andre Agassi", "American", "90222459-c5ca-436e-a8b3-b92e0669c683" },
                    { 5, 68, "Swedish legend with ice-cold nerves and six French Open titles.", "https://lavercup.com/wp-content/uploads/2022/12/figure-borg-2.png", "Björn Borg", "Swedish", "90222459-c5ca-436e-a8b3-b92e0669c683" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Coach Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Coach Name");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Coach Description",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldComment: "Coach Description");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a77e2793-8ce5-40ad-996c-c5bc6b6aa520", "AQAAAAIAAYagAAAAEOl3ndjPd65Mu1sCXDlkbxnodkzbZ4bZp1ES99RiPr4yi1HlYNxWks3r146L058SWw==", "9ff15a0f-11b7-4e52-9882-21690abb2198" });
        }
    }
}
