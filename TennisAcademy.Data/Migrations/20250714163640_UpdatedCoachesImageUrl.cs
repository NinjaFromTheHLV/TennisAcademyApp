using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCoachesImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("0f640ec0-da3a-4435-b2bb-62160a59c7b3"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("893aa403-c106-42bd-84ee-a66b909aba7a"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("89891916-c461-453b-9c9e-d6559bc26149"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("e9430eaf-7b86-4d2d-bf9c-fc20087e815d"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("f0735cef-5c6b-438c-9621-e2135bb98e1f"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e380ee2-91aa-4f8e-80fd-0e18a2445b8f", "AQAAAAIAAYagAAAAEB9DsocRp9PmFZlqgIMv6C5xxmvouZSwHFZ2k77Z3Ibk3VEvMIbdYnlVIAhymPcu0A==", "a38bc567-b37a-4c5a-87f1-a13f3da36ed2" });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "CoachId", "Age", "Description", "ImageUrl", "IsDeleted", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("01462166-f9a9-4da9-b0ce-b34b6a6517fe"), 43, "Swiss tennis legend with unmatched elegance and 20 Grand Slam titles.", "https://a.espncdn.com/combiner/i?img=/i/headshots/tennis/players/full/425.png", false, "Roger Federer", "068f642f-ce05-4f68-a9d7-5e7721595c68" },
                    { new Guid("1b80f46c-489e-4aa6-b183-510738f4e32f"), 68, "Swedish legend with ice-cold nerves and six French Open titles.", "https://lavercup.com/wp-content/uploads/2022/12/figure-borg-2.png", false, "Björn Borg", "068f642f-ce05-4f68-a9d7-5e7721595c68" },
                    { new Guid("681d9470-3ad0-4d6b-b9b4-163e7605a256"), 38, "One of the greatest tennis players of all time, known for his clay court dominance.", "https://a.espncdn.com/combiner/i?img=/i/headshots/tennis/players/full/261.png", false, "Rafael Nadal", "068f642f-ce05-4f68-a9d7-5e7721595c68" },
                    { new Guid("a13d250f-8b8d-48a8-a7ee-c0d7b5a4b314"), 37, "Serbian champion, known for his resilience and complete game.", "https://a.espncdn.com/i/headshots/tennis/players/full/296.png", false, "Novak Djokovic", "068f642f-ce05-4f68-a9d7-5e7721595c68" },
                    { new Guid("e98c7e24-e4cd-48da-8097-b7e8765519f1"), 55, "American icon who redefined tennis in the 90s with a colorful personality.", "https://www.atptour.com/-/media/alias/player-headshot/A092", false, "Andre Agassi", "068f642f-ce05-4f68-a9d7-5e7721595c68" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("01462166-f9a9-4da9-b0ce-b34b6a6517fe"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("1b80f46c-489e-4aa6-b183-510738f4e32f"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("681d9470-3ad0-4d6b-b9b4-163e7605a256"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("a13d250f-8b8d-48a8-a7ee-c0d7b5a4b314"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("e98c7e24-e4cd-48da-8097-b7e8765519f1"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45797579-f9f4-4cce-879f-0c57e1bf1473", "AQAAAAIAAYagAAAAENN+cbfrhzg6zSUSDVWD9WkfGc5IzblIoymKUXA5esKlgQwKTuXwiMLCB9pp1GgW4Q==", "3795000c-d084-4988-aa05-1d1982fda55e" });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "CoachId", "Age", "Description", "ImageUrl", "IsDeleted", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("0f640ec0-da3a-4435-b2bb-62160a59c7b3"), 38, "One of the greatest tennis players of all time, known for his clay court dominance.", "https://a.espncdn.com/combiner/i?img=/i/headshots/tennis/players/full/261.png", false, "Rafael Nadal", "068f642f-ce05-4f68-a9d7-5e7721595c68" },
                    { new Guid("893aa403-c106-42bd-84ee-a66b909aba7a"), 43, "Swiss tennis legend with unmatched elegance and 20 Grand Slam titles.", "https://www.google.com/imgres?q=roger%20federer&imgurl=https%3A%2F%2Fwww.atptour.com%2F-%2Fmedia%2Falias%2Fplayer-headshot%2Ff324&imgrefurl=https%3A%2F%2Fwww.atptour.com%2Fen%2Fplayers%2Froger-federer%2Ff324%2Foverview&docid=M6568FadTUBTgM&tbnid=2jEmAALd9M0LIM&vet=12ahUKEwjarIq4xraOAxWhBdsEHV-rA4gQM3oECHEQAA..i&w=379&h=603&hcb=2&ved=2ahUKEwjarIq4xraOAxWhBdsEHV-rA4gQM3oECHEQAA", false, "Roger Federer", "068f642f-ce05-4f68-a9d7-5e7721595c68" },
                    { new Guid("89891916-c461-453b-9c9e-d6559bc26149"), 68, "Swedish legend with ice-cold nerves and six French Open titles.", "https://www.google.com/imgres?q=bjong%20borg&imgurl=https%3A%2F%2Flavercup.com%2Fwp-content%2Fuploads%2F2022%2F12%2Ffigure-borg-2.png&imgrefurl=https%3A%2F%2Flavercup.com%2Fcaptains%2Fbjorn-borg&docid=uHC93uLecxmVaM&tbnid=ueuT_lH79uqMBM&vet=12ahUKEwi_9ISVx7aOAxXFcfEDHbCkIN0QM3oECFUQAA..i&w=506&h=495&hcb=2&ved=2ahUKEwi_9ISVx7aOAxXFcfEDHbCkIN0QM3oECFUQAA", false, "Björn Borg", "068f642f-ce05-4f68-a9d7-5e7721595c68" },
                    { new Guid("e9430eaf-7b86-4d2d-bf9c-fc20087e815d"), 55, "American icon who redefined tennis in the 90s with a colorful personality.", "https://www.google.com/imgres?q=agassi&imgurl=https%3A%2F%2Fphoto-assets.usopen.org%2Fimages%2Fpics%2Flarge%2Ff_Agassi_20240522.jpg&imgrefurl=https%3A%2F%2Fwww.usopen.org%2Fen_US%2Fnews%2Farticles%2F2024-05-22%2Fandre_agassi_to_captain_team_world_at_laver_cup_beginning_in_2025.html&docid=sNSnFHHM55CAnM&tbnid=44pBN3aOPFbpZM&vet=12ahUKEwiT05juxraOAxXzcfEDHdmfMZ8QM3oECHsQAA..i&w=1280&h=720&hcb=2&ved=2ahUKEwiT05juxraOAxXzcfEDHdmfMZ8QM3oECHsQAA", false, "Andre Agassi", "068f642f-ce05-4f68-a9d7-5e7721595c68" },
                    { new Guid("f0735cef-5c6b-438c-9621-e2135bb98e1f"), 37, "Serbian champion, known for his resilience and complete game.", "https://www.google.com/imgres?q=novak%20djokovic&imgurl=https%3A%2F%2Fa.espncdn.com%2Fi%2Fheadshots%2Ftennis%2Fplayers%2Ffull%2F296.png&imgrefurl=https%3A%2F%2Fwww.espn.com%2Ftennis%2Fplayer%2F_%2Fid%2F296%2Fnovak-djokovic&docid=2OMGcknRlaYD5M&tbnid=Up5bOK2dmBA9KM&vet=12ahUKEwjnoqLPxraOAxV4c_EDHW7-AMAQM3oECFoQAA..i&w=600&h=436&hcb=2&ved=2ahUKEwjnoqLPxraOAxV4c_EDHW7-AMAQM3oECFoQAA", false, "Novak Djokovic", "068f642f-ce05-4f68-a9d7-5e7721595c68" }
                });
        }
    }
}
