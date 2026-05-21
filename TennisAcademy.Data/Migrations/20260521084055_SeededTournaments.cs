using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeededTournaments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_TournamentCategories_CategoryId",
                schema: "22180021",
                table: "Tournaments");

            migrationBuilder.InsertData(
                schema: "22180021",
                table: "TournamentCategories",
                columns: new[] { "Id", "IsDeleted", "Name", "NameBg" },
                values: new object[,]
                {
                    { 1, false, "Singles Men", "Сингъл Мъже" },
                    { 2, false, "Singles Women", "Сингъл Жени" },
                    { 3, false, "Juniors", "Юноши" }
                });

            migrationBuilder.InsertData(
                schema: "22180021",
                table: "Tournaments",
                columns: new[] { "Id", "CategoryId", "Description", "DescriptionBg", "EndDate", "EntryFee", "IsDeleted", "MaxParticipants", "StartDate", "Title", "TitleBg" },
                values: new object[,]
                {
                    { 1, 1, "Annual spring tournament open for all non-professional male players. Format: Direct elimination.", "Годишен пролетен турнир, отворен за всички непрофесионални играчи (мъже). Формат: Директна елиминация.", new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 40.00m, false, 32, new DateTime(2026, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spring Clay Court Open", "Пролетен отворен шампионат на клей" },
                    { 2, 2, "Special dynamic tournament for women. Beautiful trophies and sponsor prizes provided.", "Специален динамичен турнир за жени. Осигурени са красиви трофеи и награди от спонсори.", new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 35.00m, false, 16, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Academy Women's Cup", "Купа на Академията за жени" },
                    { 3, 3, "Tournament targeted at young talents up to 18 years old. Great opportunity to boost local ranking points.", "Турнир, насочен към млади таланти до 18 години. Страхотна възможност за трупане на точки за местната ранглиста.", new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.00m, false, 24, new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Junior Summer Slams", "Младежки летен шлем" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_TournamentCategories_CategoryId",
                schema: "22180021",
                table: "Tournaments",
                column: "CategoryId",
                principalSchema: "22180021",
                principalTable: "TournamentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_TournamentCategories_CategoryId",
                schema: "22180021",
                table: "Tournaments");

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "TournamentCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "TournamentCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "TournamentCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_TournamentCategories_CategoryId",
                schema: "22180021",
                table: "Tournaments",
                column: "CategoryId",
                principalSchema: "22180021",
                principalTable: "TournamentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
