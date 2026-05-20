using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialFacultySchemaAndAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "22180021");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "22180021",
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
                schema: "22180021",
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
                name: "Bags",
                schema: "22180021",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Bag Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Bag Brand"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Bag Model"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Bag Price"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Available in stock"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Bag Image"),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bags", x => x.Id);
                },
                comment: "Bags Shop");

            migrationBuilder.CreateTable(
                name: "Balls",
                schema: "22180021",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Ball Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ball Brand"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ball Model"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Ball Price"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Available in stock"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Racket Image"),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balls", x => x.Id);
                },
                comment: "Balls Shop");

            migrationBuilder.CreateTable(
                name: "Coaches",
                schema: "22180021",
                columns: table => new
                {
                    CoachId = table.Column<int>(type: "int", nullable: false, comment: "Coach Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Coach Name"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Coach Image"),
                    Age = table.Column<int>(type: "int", nullable: false, comment: "Coach Age"),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Coach Description"),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Coach Nationality"),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.CoachId);
                },
                comment: "Tennis Academy Coaches");

            migrationBuilder.CreateTable(
                name: "Rackets",
                schema: "22180021",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Racket Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Racket Brand"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Racket Model"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Racket Price"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Available in stock"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Racket Image"),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rackets", x => x.Id);
                },
                comment: "Rackets Shop");

            migrationBuilder.CreateTable(
                name: "Surfaces",
                schema: "22180021",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Surface Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Surface Name"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ïmage of the surface"),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surfaces", x => x.Id);
                },
                comment: "Tennis Academy Surfaces");

            migrationBuilder.CreateTable(
                name: "Trainings",
                schema: "22180021",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Training Type Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Training Type Name"),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                },
                comment: "Tennis Academy Trainings");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "22180021",
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
                        principalSchema: "22180021",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "22180021",
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
                        principalSchema: "22180021",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "22180021",
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
                        principalSchema: "22180021",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "22180021",
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
                        principalSchema: "22180021",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "22180021",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "22180021",
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
                        principalSchema: "22180021",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BagCart",
                schema: "22180021",
                columns: table => new
                {
                    BagId = table.Column<int>(type: "int", nullable: false, comment: "Foreign Key of Bag"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign Key of IdentityUser"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Quantity of Bags in Cart"),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BagCart", x => new { x.BagId, x.UserId });
                    table.ForeignKey(
                        name: "FK_BagCart_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "22180021",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BagCart_Bags_BagId",
                        column: x => x.BagId,
                        principalSchema: "22180021",
                        principalTable: "Bags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BallCart",
                schema: "22180021",
                columns: table => new
                {
                    BallId = table.Column<int>(type: "int", nullable: false, comment: "Foreign Key of Ball"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign Key of IdentityUser"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Quantity of Balls in Cart"),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BallCart", x => new { x.BallId, x.UserId });
                    table.ForeignKey(
                        name: "FK_BallCart_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "22180021",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BallCart_Balls_BallId",
                        column: x => x.BallId,
                        principalSchema: "22180021",
                        principalTable: "Balls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Ball Cart");

            migrationBuilder.CreateTable(
                name: "UserFavourites",
                schema: "22180021",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign Key which references to IdentityUser"),
                    CoachId = table.Column<int>(type: "int", nullable: false, comment: "Foreign Key which references to Coach"),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavourites", x => new { x.UserId, x.CoachId });
                    table.ForeignKey(
                        name: "FK_UserFavourites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "22180021",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFavourites_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalSchema: "22180021",
                        principalTable: "Coaches",
                        principalColumn: "CoachId");
                },
                comment: "Users Favourite Coach");

            migrationBuilder.CreateTable(
                name: "RacketCart",
                schema: "22180021",
                columns: table => new
                {
                    RacketId = table.Column<int>(type: "int", nullable: false, comment: "Foreign Key of Racket"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign Key of IdentityUser"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Quantity of Rackets in Cart"),
                    BallId = table.Column<int>(type: "int", nullable: true),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacketCart", x => new { x.RacketId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RacketCart_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "22180021",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RacketCart_Balls_BallId",
                        column: x => x.BallId,
                        principalSchema: "22180021",
                        principalTable: "Balls",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RacketCart_Rackets_RacketId",
                        column: x => x.RacketId,
                        principalSchema: "22180021",
                        principalTable: "Rackets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Racket Cart");

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "22180021",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Reservation Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true, comment: "Player Notes"),
                    SurfaceId = table.Column<int>(type: "int", nullable: false, comment: "Choosing a surface"),
                    CoachId = table.Column<int>(type: "int", nullable: false, comment: "Choosing a coach"),
                    TrainingTypeId = table.Column<int>(type: "int", nullable: false, comment: "Choosing a training type"),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Player Identifer"),
                    Duration = table.Column<int>(type: "int", nullable: false, comment: "Duration of the session"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date Select"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModified_22180021 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "22180021",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalSchema: "22180021",
                        principalTable: "Coaches",
                        principalColumn: "CoachId");
                    table.ForeignKey(
                        name: "FK_Reservations_Surfaces_SurfaceId",
                        column: x => x.SurfaceId,
                        principalSchema: "22180021",
                        principalTable: "Surfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Trainings_TrainingTypeId",
                        column: x => x.TrainingTypeId,
                        principalSchema: "22180021",
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Player Reservations");

            migrationBuilder.InsertData(
                schema: "22180021",
                table: "Bags",
                columns: new[] { "Id", "Brand", "ImageUrl", "Model", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Wilson", "https://cdn.media.amplience.net/i/sportinglife/25918789_0/Team-3-Pack-Tennis-Bag?$default$&fmt=auto&w=540&h=540", "Team 3-Pack", 59.99m, 10 },
                    { 2, "Head", "https://media.strefatenisa.com.pl/public/media/20/c1/2b/1721072068/head-tour-team-6r-combi-black-mixed-1.jpg?ts=1745860751", "Tour Team 6R", 89.99m, 7 },
                    { 3, "Babolat", "https://m.media-amazon.com/images/I/61vGrieRbCL._UF1000,1000_QL80_.jpg", "Pure Drive RHx6", 99.99m, 5 },
                    { 4, "Yonex", "https://www.midwestracquetsports.com/images/xl/BAG92429BK.jpg?v=1", "Pro Series 9-Pack", 129.99m, 4 }
                });

            migrationBuilder.InsertData(
                schema: "22180021",
                table: "Balls",
                columns: new[] { "Id", "Brand", "ImageUrl", "Model", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Wilson", "https://m.media-amazon.com/images/I/715MEN61aPL._UF1000,1000_QL80_.jpg", "US Open Extra Duty", 12.99m, 50 },
                    { 2, "Head", "https://cdn.sportdepot.bg/files/catalog/detail/570823_01.jpg", "Tour XT", 11.49m, 35 },
                    { 3, "Dunlop", "https://m.media-amazon.com/images/I/618MvroxyXL._UF1000,1000_QL80_.jpg", "ATP Championship", 10.99m, 40 }
                });

            migrationBuilder.InsertData(
                schema: "22180021",
                table: "Coaches",
                columns: new[] { "CoachId", "Age", "Description", "ImageUrl", "Name", "Nationality" },
                values: new object[,]
                {
                    { 1, 38, "One of the greatest tennis players of all time, known for his clay court dominance.", "~/pictures/rafa.jpg", "Rafael Nadal", "Spanish" },
                    { 2, 43, "Swiss tennis legend with unmatched elegance and 20 Grand Slam titles.", "https://a.espncdn.com/combiner/i?img=/i/headshots/tennis/players/full/425.png", "Roger Federer", "Swiss" },
                    { 3, 37, "Serbian champion, known for his resilience and complete game.", "https://a.espncdn.com/i/headshots/tennis/players/full/296.png", "Novak Djokovic", "Serbian" },
                    { 4, 55, "American icon who redefined tennis in the 90s with a colorful personality.", "https://www.atptour.com/-/media/alias/player-headshot/A092", "Andre Agassi", "American" },
                    { 5, 68, "Swedish legend with ice-cold nerves and six French Open titles.", "https://lavercup.com/wp-content/uploads/2022/12/figure-borg-2.png", "Björn Borg", "Swedish" }
                });

            migrationBuilder.InsertData(
                schema: "22180021",
                table: "Rackets",
                columns: new[] { "Id", "Brand", "ImageUrl", "Model", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Wilson", "https://cdncloudcart.com/28710/products/images/134337/tenis-raketa-wilson-pro-staff-rf-97-v13-0-tns-fr-image_6358bfebb40a9_800x800.jpeg?1666760684", "Pro Staff 97", 349.99m, 5 },
                    { 2, "Babolat", "https://babolat.bg/image/cache/catalog/tennis/2024/rackets/101474/101474-Pure_Drive_98-136-1-Face_2-250x250.jpg", "Pure Drive", 299.99m, 8 },
                    { 3, "Head", "https://i.sportisimo.com/products/images/1104/1104555/700x700/head-graphene-360-speed-mp_1.jpg", "Graphene 360+ Speed", 279.99m, 10 },
                    { 4, "Yonex", "https://us.yonex.com/cdn/shop/files/EZ0898_BlastBlue_5868.jpg?v=1739481973&width=1946", "Ezone 98", 319.99m, 6 },
                    { 5, "Prince", "https://images.squarespace-cdn.com/content/v1/56e9b38c2b8dde820241b62d/1471886555425-JT9KKFKPOL4FNLAV9ZB0/r2.jpg", "Tour 100P", 259.99m, 4 },
                    { 6, "Tecnifibre", "https://www.tecnifibre.com/dw/image/v2/BHDN_PRD/on/demandware.static/-/Sites-tecnifibre-master-catalog/default/dwcf93310b/hi-res/T-FIGHT%202025/Packshots/305S/14FI305S5_04.jpg?sw=608&sh=608&sm=fit", "TFight 305", 289.99m, 7 }
                });

            migrationBuilder.InsertData(
                schema: "22180021",
                table: "Surfaces",
                columns: new[] { "Id", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "https://www.edwardssports.co.uk/pub/media/magefan_blog/Clay_Tennis_Courts.jpg", "Clay" },
                    { 2, "https://www.tennisnerd.net/wp-content/uploads/2024/06/grass-tennis.webp", "Grass" },
                    { 3, "https://asltenniscourts.com.au/wp-content/uploads/2021/03/AdobeStock_253105355-1024x683.jpeg", "Hard" }
                });

            migrationBuilder.InsertData(
                schema: "22180021",
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
                schema: "22180021",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "22180021",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "22180021",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "22180021",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "22180021",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "22180021",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "22180021",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BagCart_UserId",
                schema: "22180021",
                table: "BagCart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BallCart_UserId",
                schema: "22180021",
                table: "BallCart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RacketCart_BallId",
                schema: "22180021",
                table: "RacketCart",
                column: "BallId");

            migrationBuilder.CreateIndex(
                name: "IX_RacketCart_UserId",
                schema: "22180021",
                table: "RacketCart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CoachId",
                schema: "22180021",
                table: "Reservations",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PlayerId",
                schema: "22180021",
                table: "Reservations",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SurfaceId",
                schema: "22180021",
                table: "Reservations",
                column: "SurfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TrainingTypeId",
                schema: "22180021",
                table: "Reservations",
                column: "TrainingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavourites_CoachId",
                schema: "22180021",
                table: "UserFavourites",
                column: "CoachId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "BagCart",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "BallCart",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "RacketCart",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "UserFavourites",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "Bags",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "Balls",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "Rackets",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "Surfaces",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "Trainings",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "22180021");

            migrationBuilder.DropTable(
                name: "Coaches",
                schema: "22180021");
        }
    }
}
