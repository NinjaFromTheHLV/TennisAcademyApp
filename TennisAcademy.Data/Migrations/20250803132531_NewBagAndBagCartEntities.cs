using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewBagAndBagCartEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Bag Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Bag Brand"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Bag Model"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, comment: "Bag Price"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Available in stock"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Bag Image")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bags", x => x.Id);
                },
                comment: "Bags Shop");

            migrationBuilder.CreateTable(
                name: "BagCart",
                columns: table => new
                {
                    BagId = table.Column<int>(type: "int", nullable: false, comment: "Foreign Key of Bag"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign Key of IdentityUser"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Quantity of Bags in Cart")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BagCart", x => new { x.BagId, x.UserId });
                    table.ForeignKey(
                        name: "FK_BagCart_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BagCart_Bags_BagId",
                        column: x => x.BagId,
                        principalTable: "Bags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BagCart_UserId",
                table: "BagCart",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BagCart");

            migrationBuilder.DropTable(
                name: "Bags");
        }
    }
}
