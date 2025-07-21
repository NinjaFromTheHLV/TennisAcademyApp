using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surfaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Surface Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Surface Name"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ïmage of the surface")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surfaces", x => x.Id);
                },
                comment: "Tennis Academy Surfaces");

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Training Type Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Training Type Name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                },
                comment: "Tennis Academy Trainings");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    CoachId = table.Column<int>(type: "int", nullable: false, comment: "Coach Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Coach Name"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Coach Image"),
                    Age = table.Column<int>(type: "int", nullable: false, comment: "Coach Age"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Coach Description"),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Coach Nationality"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key of IdentityUser")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.CoachId);
                    table.ForeignKey(
                        name: "FK_Coaches_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Tennis Academy Coaches");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Reservation Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true, comment: "Player Notes"),
                    SurfaceId = table.Column<int>(type: "int", nullable: false, comment: "Choosing a surface"),
                    CoachId = table.Column<int>(type: "int", nullable: false, comment: "Choosing a coach"),
                    TrainingTypeId = table.Column<int>(type: "int", nullable: false, comment: "Choosing a training type"),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Player Identifer"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date Select")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "CoachId");
                    table.ForeignKey(
                        name: "FK_Reservations_Surfaces_SurfaceId",
                        column: x => x.SurfaceId,
                        principalTable: "Surfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Trainings_TrainingTypeId",
                        column: x => x.TrainingTypeId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Player Reservations");

            migrationBuilder.CreateTable(
                name: "UserFavourites",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign Key which references to IdentityUser"),
                    CoachId = table.Column<int>(type: "int", nullable: false, comment: "Foreign Key which references to IdentityUser")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavourites", x => new { x.UserId, x.CoachId });
                    table.ForeignKey(
                        name: "FK_UserFavourites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFavourites_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "CoachId");
                },
                comment: "Users Favourite Coach");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "seed-user-id-123", 0, "a77e2793-8ce5-40ad-996c-c5bc6b6aa520", "coachadmin@example.com", true, false, null, "COACHADMIN@EXAMPLE.COM", "COACHADMIN", "AQAAAAIAAYagAAAAEOl3ndjPd65Mu1sCXDlkbxnodkzbZ4bZp1ES99RiPr4yi1HlYNxWks3r146L058SWw==", null, false, "9ff15a0f-11b7-4e52-9882-21690abb2198", false, "coachadmin" });

            migrationBuilder.InsertData(
                table: "Surfaces",
                columns: new[] { "Id", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "https://www.google.com/imgres?q=clay%20court&imgurl=https%3A%2F%2Fcdn11.bigcommerce.com%2Fs-pop81y%2Fimages%2Fstencil%2F960x545%2Fuploaded_images%2Fallstartennissupply-281535-clay-tennis-courts-blogbanner1.jpg%3Ft%3D1703002083&imgrefurl=https%3A%2F%2Fwww.allstartennissupply.com%2Fblog%2Fwhat-is-the-best-climate-for-clay-tennis-courts%2F%3Fsrsltid%3DAfmBOorm03gyRg52IMAFa7-l2ig3k_9l9SE1UjjQCmsplj7SJUMqY2Ci&docid=wGtAwbkIqo2SLM&tbnid=587J-uncERjX8M&vet=12ahUKEwjRzYidybaOAxVeX_EDHZpnGxwQM3oECFEQAA..i&w=960&h=539&hcb=2&ved=2ahUKEwjRzYidybaOAxVeX_EDHZpnGxwQM3oECFEQAA", "Clay" },
                    { 2, "https://www.google.com/imgres?q=Hard%20court&imgurl=https%3A%2F%2Fwww.edwardssports.co.uk%2Fpub%2Fmedia%2Fwysiwyg%2FPlaying_On_A_Hard_Tennis_Court.jpg&imgrefurl=https%3A%2F%2Fwww.edwardssports.co.uk%2Fnews%2Fpost%2Fclay-court-vs-hard-court-tennis&docid=_e_VzZEOyVdxeM&tbnid=O670DpSc8HOqTM&vet=12ahUKEwir89etybaOAxW6evEDHSxsBQ0QM3oECB0QAA..i&w=900&h=500&hcb=2&ved=2ahUKEwir89etybaOAxW6evEDHSxsBQ0QM3oECB0QAA", "Hard" },
                    { 3, "https://www.google.com/imgres?q=Grass%20court&imgurl=https%3A%2F%2Fi.abcnewsfe.com%2Fa%2F172020d0-16bb-4c84-a3d2-b436b77d5f7e%2Fwimbledon5-2023-gty-ml-240614_1718369987464_hpMain.jpg&imgrefurl=https%3A%2F%2Fabcnews.go.com%2FUS%2Fstaggering-science-art-wimbledons-legendary-grass-courts%2Fstory%3Fid%3D111433116&docid=7Ne81Wn1LUAVtM&tbnid=f5ihWuIF1wvqzM&vet=12ahUKEwjyxZTSybaOAxVaSfEDHU1jGa4QM3oECBoQAA..i&w=3072&h=2048&hcb=2&ved=2ahUKEwjyxZTSybaOAxVaSfEDHU1jGa4QM3oECBoQAA", "Grass" }
                });

            migrationBuilder.InsertData(
                table: "Trainings",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Physical Conditioning Routine" },
                    { 2, "Technical Skill Development" },
                    { 3, "Tactical Game Strategy" },
                    { 4, "Mental Toughness Training" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_UserId",
                table: "Coaches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CoachId",
                table: "Reservations",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PlayerId",
                table: "Reservations",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SurfaceId",
                table: "Reservations",
                column: "SurfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TrainingTypeId",
                table: "Reservations",
                column: "TrainingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavourites_CoachId",
                table: "UserFavourites",
                column: "CoachId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "UserFavourites");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Surfaces");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
