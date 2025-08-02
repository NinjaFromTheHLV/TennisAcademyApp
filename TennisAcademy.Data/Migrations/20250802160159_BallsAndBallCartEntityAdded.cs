using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class BallsAndBallCartEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RacketCart_AspNetUsers_UserId",
                table: "RacketCart");

            migrationBuilder.DropForeignKey(
                name: "FK_RacketCart_Rackets_RacketId",
                table: "RacketCart");

            migrationBuilder.CreateTable(
                name: "Balls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Ball Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ball Brand"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Ball Model"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Ball Price"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Available in stock"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Racket Image")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balls", x => x.Id);
                },
                comment: "Balls Shop");

            migrationBuilder.CreateTable(
                name: "BallCart",
                columns: table => new
                {
                    BallId = table.Column<int>(type: "int", nullable: false, comment: "Foreign Key of Ball"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign Key of IdentityUser"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Quantity of Balls in Cart")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BallCart", x => new { x.BallId, x.UserId });
                    table.ForeignKey(
                        name: "FK_BallCart_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BallCart_Balls_BallId",
                        column: x => x.BallId,
                        principalTable: "Balls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Ball Cart");

            migrationBuilder.InsertData(
                table: "Balls",
                columns: new[] { "Id", "Brand", "ImageUrl", "Model", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Wilson", "https://m.media-amazon.com/images/I/715MEN61aPL._UF1000,1000_QL80_.jpg", "US Open Extra Duty", 12.99m, 50 },
                    { 2, "Head", "https://cdn.sportdepot.bg/files/catalog/detail/570823_01.jpg", "Tour XT", 11.49m, 35 },
                    { 3, "Dunlop", "https://m.media-amazon.com/images/I/618MvroxyXL._UF1000,1000_QL80_.jpg", "ATP Championship", 10.99m, 40 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BallCart_UserId",
                table: "BallCart",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RacketCart_AspNetUsers_UserId",
                table: "RacketCart",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RacketCart_Rackets_RacketId",
                table: "RacketCart",
                column: "RacketId",
                principalTable: "Rackets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RacketCart_AspNetUsers_UserId",
                table: "RacketCart");

            migrationBuilder.DropForeignKey(
                name: "FK_RacketCart_Rackets_RacketId",
                table: "RacketCart");

            migrationBuilder.DropTable(
                name: "BallCart");

            migrationBuilder.DropTable(
                name: "Balls");

            migrationBuilder.AddForeignKey(
                name: "FK_RacketCart_AspNetUsers_UserId",
                table: "RacketCart",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RacketCart_Rackets_RacketId",
                table: "RacketCart",
                column: "RacketId",
                principalTable: "Rackets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
