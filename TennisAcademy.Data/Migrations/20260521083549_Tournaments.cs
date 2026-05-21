using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Tournaments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TournamentCategories",
                schema: "22180021",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameBg = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                schema: "22180021",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleBg = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionBg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntryFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_TournamentCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "22180021",
                        principalTable: "TournamentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TournamentsUsers",
                schema: "22180021",
                columns: table => new
                {
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnrolledOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentsUsers", x => new { x.TournamentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TournamentsUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "22180021",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentsUsers_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalSchema: "22180021",
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_CategoryId",
                schema: "22180021",
                table: "Tournaments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentsUsers_UserId",
                schema: "22180021",
                table: "TournamentsUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentsUsers",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "Tournaments",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "TournamentCategories",
                schema: "22180021");
        }
    }
}
