using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SurfaceImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e909aa46-df31-4dad-8863-216361afcfd9", "AQAAAAIAAYagAAAAENSPTocZsWupEqiI5gVLElxy5Hv5vYLdHTtw4Oy0I8rr8LlxQuDU7owCIPqV9v56LQ==", "e64463ec-2510-4265-a911-c60cf20ee29a" });

            migrationBuilder.UpdateData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.edwardssports.co.uk/pub/media/magefan_blog/Clay_Tennis_Courts.jpg");

            migrationBuilder.UpdateData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://asltenniscourts.com.au/wp-content/uploads/2021/03/AdobeStock_253105355-1024x683.jpeg");

            migrationBuilder.UpdateData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://www.tennisnerd.net/wp-content/uploads/2024/06/grass-tennis.webp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba41741c-10c8-470f-8f7c-8ad7d1cfc297", "AQAAAAIAAYagAAAAEAIhc/AjxYkB6HbNbNZAIQJbwgqnDbMZPNJlTMfqcDm4xf0o2eVNe4Nvts3E8IfOaA==", "51260f59-16e2-4821-b172-373e58dbdcb6" });

            migrationBuilder.UpdateData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.google.com/imgres?q=clay%20court&imgurl=https%3A%2F%2Fcdn11.bigcommerce.com%2Fs-pop81y%2Fimages%2Fstencil%2F960x545%2Fuploaded_images%2Fallstartennissupply-281535-clay-tennis-courts-blogbanner1.jpg%3Ft%3D1703002083&imgrefurl=https%3A%2F%2Fwww.allstartennissupply.com%2Fblog%2Fwhat-is-the-best-climate-for-clay-tennis-courts%2F%3Fsrsltid%3DAfmBOorm03gyRg52IMAFa7-l2ig3k_9l9SE1UjjQCmsplj7SJUMqY2Ci&docid=wGtAwbkIqo2SLM&tbnid=587J-uncERjX8M&vet=12ahUKEwjRzYidybaOAxVeX_EDHZpnGxwQM3oECFEQAA..i&w=960&h=539&hcb=2&ved=2ahUKEwjRzYidybaOAxVeX_EDHZpnGxwQM3oECFEQAA");

            migrationBuilder.UpdateData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://www.google.com/imgres?q=Hard%20court&imgurl=https%3A%2F%2Fwww.edwardssports.co.uk%2Fpub%2Fmedia%2Fwysiwyg%2FPlaying_On_A_Hard_Tennis_Court.jpg&imgrefurl=https%3A%2F%2Fwww.edwardssports.co.uk%2Fnews%2Fpost%2Fclay-court-vs-hard-court-tennis&docid=_e_VzZEOyVdxeM&tbnid=O670DpSc8HOqTM&vet=12ahUKEwir89etybaOAxW6evEDHSxsBQ0QM3oECB0QAA..i&w=900&h=500&hcb=2&ved=2ahUKEwir89etybaOAxW6evEDHSxsBQ0QM3oECB0QAA");

            migrationBuilder.UpdateData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://www.google.com/imgres?q=Grass%20court&imgurl=https%3A%2F%2Fi.abcnewsfe.com%2Fa%2F172020d0-16bb-4c84-a3d2-b436b77d5f7e%2Fwimbledon5-2023-gty-ml-240614_1718369987464_hpMain.jpg&imgrefurl=https%3A%2F%2Fabcnews.go.com%2FUS%2Fstaggering-science-art-wimbledons-legendary-grass-courts%2Fstory%3Fid%3D111433116&docid=7Ne81Wn1LUAVtM&tbnid=f5ihWuIF1wvqzM&vet=12ahUKEwjyxZTSybaOAxVaSfEDHU1jGa4QM3oECBoQAA..i&w=3072&h=2048&hcb=2&ved=2ahUKEwjyxZTSybaOAxVaSfEDHU1jGa4QM3oECBoQAA");
        }
    }
}
