using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeededUserAndCoaches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "seed-user-id-123", 0, "eaf473c9-4360-4fee-953d-9e3640817b32", "coachadmin@example.com", true, false, null, "COACHADMIN@EXAMPLE.COM", "COACHADMIN", "AQAAAAIAAYagAAAAELAChf5L0uFAPYNNA48OVs1jcdkVZG6ksA3wpE8nyHToDX4zg2FWy/NIg57FAAgc+Q==", null, false, "83d3d211-7d23-4389-a5f3-0652ce6d0391", false, "coachadmin" });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "CoachId", "Age", "Description", "ImageUrl", "IsDeleted", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("3bef8abd-5089-43c7-8319-1ee6c7c9a0e6"), 55, "American icon who redefined tennis in the 90s with a colorful personality.", "https://www.google.com/imgres?q=agassi&imgurl=https%3A%2F%2Fphoto-assets.usopen.org%2Fimages%2Fpics%2Flarge%2Ff_Agassi_20240522.jpg&imgrefurl=https%3A%2F%2Fwww.usopen.org%2Fen_US%2Fnews%2Farticles%2F2024-05-22%2Fandre_agassi_to_captain_team_world_at_laver_cup_beginning_in_2025.html&docid=sNSnFHHM55CAnM&tbnid=44pBN3aOPFbpZM&vet=12ahUKEwiT05juxraOAxXzcfEDHdmfMZ8QM3oECHsQAA..i&w=1280&h=720&hcb=2&ved=2ahUKEwiT05juxraOAxXzcfEDHdmfMZ8QM3oECHsQAA", false, "Andre Agassi", "seed-user-id-123" },
                    { new Guid("5ce8140e-77da-4894-b1e7-126437240ea3"), 37, "Serbian champion, known for his resilience and complete game.", "https://www.google.com/imgres?q=novak%20djokovic&imgurl=https%3A%2F%2Fa.espncdn.com%2Fi%2Fheadshots%2Ftennis%2Fplayers%2Ffull%2F296.png&imgrefurl=https%3A%2F%2Fwww.espn.com%2Ftennis%2Fplayer%2F_%2Fid%2F296%2Fnovak-djokovic&docid=2OMGcknRlaYD5M&tbnid=Up5bOK2dmBA9KM&vet=12ahUKEwjnoqLPxraOAxV4c_EDHW7-AMAQM3oECFoQAA..i&w=600&h=436&hcb=2&ved=2ahUKEwjnoqLPxraOAxV4c_EDHW7-AMAQM3oECFoQAA", false, "Novak Djokovic", "seed-user-id-123" },
                    { new Guid("7bb88051-efdd-48a3-8964-2186dbef6dfa"), 38, "One of the greatest tennis players of all time, known for his clay court dominance.", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.espn.com%2Ftennis%2Fplayer%2F_%2Fid%2F261%2Frafael-nadal&psig=AOvVaw1byPY1iFTVP2YEgwPqBdiX&ust=1752383170168000&source=images&cd=vfe&opi=89978449&ved=0CBAQjRxqFwoTCLj_4ZzGto4DFQAAAAAdAAAAABAE", false, "Rafael Nadal", "seed-user-id-123" },
                    { new Guid("b97e8c83-9f90-4a1c-8623-26d3a219e039"), 68, "Swedish legend with ice-cold nerves and six French Open titles.", "https://www.google.com/imgres?q=bjong%20borg&imgurl=https%3A%2F%2Flavercup.com%2Fwp-content%2Fuploads%2F2022%2F12%2Ffigure-borg-2.png&imgrefurl=https%3A%2F%2Flavercup.com%2Fcaptains%2Fbjorn-borg&docid=uHC93uLecxmVaM&tbnid=ueuT_lH79uqMBM&vet=12ahUKEwi_9ISVx7aOAxXFcfEDHbCkIN0QM3oECFUQAA..i&w=506&h=495&hcb=2&ved=2ahUKEwi_9ISVx7aOAxXFcfEDHbCkIN0QM3oECFUQAA", false, "Björn Borg", "seed-user-id-123" },
                    { new Guid("f0899335-60d8-444a-8483-01f059fb2c36"), 43, "Swiss tennis legend with unmatched elegance and 20 Grand Slam titles.", "https://www.google.com/imgres?q=roger%20federer&imgurl=https%3A%2F%2Fwww.atptour.com%2F-%2Fmedia%2Falias%2Fplayer-headshot%2Ff324&imgrefurl=https%3A%2F%2Fwww.atptour.com%2Fen%2Fplayers%2Froger-federer%2Ff324%2Foverview&docid=M6568FadTUBTgM&tbnid=2jEmAALd9M0LIM&vet=12ahUKEwjarIq4xraOAxWhBdsEHV-rA4gQM3oECHEQAA..i&w=379&h=603&hcb=2&ved=2ahUKEwjarIq4xraOAxWhBdsEHV-rA4gQM3oECHEQAA", false, "Roger Federer", "seed-user-id-123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("3bef8abd-5089-43c7-8319-1ee6c7c9a0e6"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("5ce8140e-77da-4894-b1e7-126437240ea3"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("7bb88051-efdd-48a3-8964-2186dbef6dfa"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("b97e8c83-9f90-4a1c-8623-26d3a219e039"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("f0899335-60d8-444a-8483-01f059fb2c36"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123");
        }
    }
}
