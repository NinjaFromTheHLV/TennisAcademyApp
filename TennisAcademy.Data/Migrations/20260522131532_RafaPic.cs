using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RafaPic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://imageio.forbes.com/specials-images/imageserve/5ece8a5c938ec500060aae37/0x0.jpg?format=jpg&crop=2462,2460,x503,y156,safe&height=416&width=416&fit=bounds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.google.com/imgres?q=rafael%20nadal&imgurl=https%3A%2F%2Fwww.aurumbureau.com%2Fwp-content%2Fuploads%2F2025%2F12%2FAurum-Speakers-Bureau-Rafael-Nadal.webp&imgrefurl=https%3A%2F%2Fwww.aurumbureau.com%2Fspeaker%2Frafael-nadal%2F&docid=rnVCjCI2wE0PgM&tbnid=bqkPNyBAB-NHPM&vet=12ahUKEwibore2-8yUAxUySvEDHSeyOygQnPAOegQIdxAB..i&w=680&h=680&hcb=2&ved=2ahUKEwibore2-8yUAxUySvEDHSeyOygQnPAOegQIdxAB");
        }
    }
}
