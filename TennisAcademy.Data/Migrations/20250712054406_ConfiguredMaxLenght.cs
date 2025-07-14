using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguredMaxLenght : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("1f8cd12b-c022-4ed0-a62c-66d6284ff7f4"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("586089ce-adda-4c6b-9445-c5617e5b7612"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("74286cd8-9731-4339-aea0-ed7afce5e665"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("98e3c2ac-53be-40dc-8c3f-c4c3ebea46b4"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("bb73ec28-aa4b-4e17-98dd-599865156ca7"));

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Reservations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "Player Notes",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Player Notes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coaches",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: false,
                comment: "Coach Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Coach Name");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Coaches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Coach Description",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Coach Description");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "364d6ba7-08f7-4619-8358-cb23674d45b0", "AQAAAAIAAYagAAAAELhxaar4A10oAJyVavK50D6LOoa50s8krxV0OXqM8ijR1C2jV1KtVlD7fAmwu2xN+Q==", "d36b3cdc-87b4-4c7f-bdf0-d7b17b17656c" });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "CoachId", "Age", "Description", "ImageUrl", "IsDeleted", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("1559ca2d-7991-4b84-ad1c-2bc3092c55fd"), 37, "Serbian champion, known for his resilience and complete game.", "https://www.google.com/imgres?q=novak%20djokovic&imgurl=https%3A%2F%2Fa.espncdn.com%2Fi%2Fheadshots%2Ftennis%2Fplayers%2Ffull%2F296.png&imgrefurl=https%3A%2F%2Fwww.espn.com%2Ftennis%2Fplayer%2F_%2Fid%2F296%2Fnovak-djokovic&docid=2OMGcknRlaYD5M&tbnid=Up5bOK2dmBA9KM&vet=12ahUKEwjnoqLPxraOAxV4c_EDHW7-AMAQM3oECFoQAA..i&w=600&h=436&hcb=2&ved=2ahUKEwjnoqLPxraOAxV4c_EDHW7-AMAQM3oECFoQAA", false, "Novak Djokovic", "seed-user-id-123" },
                    { new Guid("3ee2e22f-0415-4baa-8b93-ff3b236f9bb6"), 43, "Swiss tennis legend with unmatched elegance and 20 Grand Slam titles.", "https://www.google.com/imgres?q=roger%20federer&imgurl=https%3A%2F%2Fwww.atptour.com%2F-%2Fmedia%2Falias%2Fplayer-headshot%2Ff324&imgrefurl=https%3A%2F%2Fwww.atptour.com%2Fen%2Fplayers%2Froger-federer%2Ff324%2Foverview&docid=M6568FadTUBTgM&tbnid=2jEmAALd9M0LIM&vet=12ahUKEwjarIq4xraOAxWhBdsEHV-rA4gQM3oECHEQAA..i&w=379&h=603&hcb=2&ved=2ahUKEwjarIq4xraOAxWhBdsEHV-rA4gQM3oECHEQAA", false, "Roger Federer", "seed-user-id-123" },
                    { new Guid("5b428151-ee0d-41e9-86b8-ef20627a5f25"), 55, "American icon who redefined tennis in the 90s with a colorful personality.", "https://www.google.com/imgres?q=agassi&imgurl=https%3A%2F%2Fphoto-assets.usopen.org%2Fimages%2Fpics%2Flarge%2Ff_Agassi_20240522.jpg&imgrefurl=https%3A%2F%2Fwww.usopen.org%2Fen_US%2Fnews%2Farticles%2F2024-05-22%2Fandre_agassi_to_captain_team_world_at_laver_cup_beginning_in_2025.html&docid=sNSnFHHM55CAnM&tbnid=44pBN3aOPFbpZM&vet=12ahUKEwiT05juxraOAxXzcfEDHdmfMZ8QM3oECHsQAA..i&w=1280&h=720&hcb=2&ved=2ahUKEwiT05juxraOAxXzcfEDHdmfMZ8QM3oECHsQAA", false, "Andre Agassi", "seed-user-id-123" },
                    { new Guid("82c2cc31-fc88-462b-9613-e6f7f2525b31"), 68, "Swedish legend with ice-cold nerves and six French Open titles.", "https://www.google.com/imgres?q=bjong%20borg&imgurl=https%3A%2F%2Flavercup.com%2Fwp-content%2Fuploads%2F2022%2F12%2Ffigure-borg-2.png&imgrefurl=https%3A%2F%2Flavercup.com%2Fcaptains%2Fbjorn-borg&docid=uHC93uLecxmVaM&tbnid=ueuT_lH79uqMBM&vet=12ahUKEwi_9ISVx7aOAxXFcfEDHbCkIN0QM3oECFUQAA..i&w=506&h=495&hcb=2&ved=2ahUKEwi_9ISVx7aOAxXFcfEDHbCkIN0QM3oECFUQAA", false, "Björn Borg", "seed-user-id-123" },
                    { new Guid("b72c5154-68dc-4baa-bd2a-a787bf198d57"), 38, "One of the greatest tennis players of all time, known for his clay court dominance.", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.espn.com%2Ftennis%2Fplayer%2F_%2Fid%2F261%2Frafael-nadal&psig=AOvVaw1byPY1iFTVP2YEgwPqBdiX&ust=1752383170168000&source=images&cd=vfe&opi=89978449&ved=0CBAQjRxqFwoTCLj_4ZzGto4DFQAAAAAdAAAAABAE", false, "Rafael Nadal", "seed-user-id-123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("1559ca2d-7991-4b84-ad1c-2bc3092c55fd"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("3ee2e22f-0415-4baa-8b93-ff3b236f9bb6"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("5b428151-ee0d-41e9-86b8-ef20627a5f25"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("82c2cc31-fc88-462b-9613-e6f7f2525b31"));

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: new Guid("b72c5154-68dc-4baa-bd2a-a787bf198d57"));

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Player Notes",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "Player Notes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Coach Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(35)",
                oldMaxLength: 35,
                oldComment: "Coach Name");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Coach Description",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Coach Description");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f7bb1bd-d658-49ad-b644-9803e69a9a09", "AQAAAAIAAYagAAAAELYqpYYjJkS+AjQ/i29XymbGaq/ZPTxhRqe0v4vRn+D7HCrY6rRgFs8TTjDwHQIjnA==", "685c4bec-6ced-421d-85cf-3347a41f2139" });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "CoachId", "Age", "Description", "ImageUrl", "IsDeleted", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("1f8cd12b-c022-4ed0-a62c-66d6284ff7f4"), 38, "One of the greatest tennis players of all time, known for his clay court dominance.", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.espn.com%2Ftennis%2Fplayer%2F_%2Fid%2F261%2Frafael-nadal&psig=AOvVaw1byPY1iFTVP2YEgwPqBdiX&ust=1752383170168000&source=images&cd=vfe&opi=89978449&ved=0CBAQjRxqFwoTCLj_4ZzGto4DFQAAAAAdAAAAABAE", false, "Rafael Nadal", "seed-user-id-123" },
                    { new Guid("586089ce-adda-4c6b-9445-c5617e5b7612"), 37, "Serbian champion, known for his resilience and complete game.", "https://www.google.com/imgres?q=novak%20djokovic&imgurl=https%3A%2F%2Fa.espncdn.com%2Fi%2Fheadshots%2Ftennis%2Fplayers%2Ffull%2F296.png&imgrefurl=https%3A%2F%2Fwww.espn.com%2Ftennis%2Fplayer%2F_%2Fid%2F296%2Fnovak-djokovic&docid=2OMGcknRlaYD5M&tbnid=Up5bOK2dmBA9KM&vet=12ahUKEwjnoqLPxraOAxV4c_EDHW7-AMAQM3oECFoQAA..i&w=600&h=436&hcb=2&ved=2ahUKEwjnoqLPxraOAxV4c_EDHW7-AMAQM3oECFoQAA", false, "Novak Djokovic", "seed-user-id-123" },
                    { new Guid("74286cd8-9731-4339-aea0-ed7afce5e665"), 43, "Swiss tennis legend with unmatched elegance and 20 Grand Slam titles.", "https://www.google.com/imgres?q=roger%20federer&imgurl=https%3A%2F%2Fwww.atptour.com%2F-%2Fmedia%2Falias%2Fplayer-headshot%2Ff324&imgrefurl=https%3A%2F%2Fwww.atptour.com%2Fen%2Fplayers%2Froger-federer%2Ff324%2Foverview&docid=M6568FadTUBTgM&tbnid=2jEmAALd9M0LIM&vet=12ahUKEwjarIq4xraOAxWhBdsEHV-rA4gQM3oECHEQAA..i&w=379&h=603&hcb=2&ved=2ahUKEwjarIq4xraOAxWhBdsEHV-rA4gQM3oECHEQAA", false, "Roger Federer", "seed-user-id-123" },
                    { new Guid("98e3c2ac-53be-40dc-8c3f-c4c3ebea46b4"), 55, "American icon who redefined tennis in the 90s with a colorful personality.", "https://www.google.com/imgres?q=agassi&imgurl=https%3A%2F%2Fphoto-assets.usopen.org%2Fimages%2Fpics%2Flarge%2Ff_Agassi_20240522.jpg&imgrefurl=https%3A%2F%2Fwww.usopen.org%2Fen_US%2Fnews%2Farticles%2F2024-05-22%2Fandre_agassi_to_captain_team_world_at_laver_cup_beginning_in_2025.html&docid=sNSnFHHM55CAnM&tbnid=44pBN3aOPFbpZM&vet=12ahUKEwiT05juxraOAxXzcfEDHdmfMZ8QM3oECHsQAA..i&w=1280&h=720&hcb=2&ved=2ahUKEwiT05juxraOAxXzcfEDHdmfMZ8QM3oECHsQAA", false, "Andre Agassi", "seed-user-id-123" },
                    { new Guid("bb73ec28-aa4b-4e17-98dd-599865156ca7"), 68, "Swedish legend with ice-cold nerves and six French Open titles.", "https://www.google.com/imgres?q=bjong%20borg&imgurl=https%3A%2F%2Flavercup.com%2Fwp-content%2Fuploads%2F2022%2F12%2Ffigure-borg-2.png&imgrefurl=https%3A%2F%2Flavercup.com%2Fcaptains%2Fbjorn-borg&docid=uHC93uLecxmVaM&tbnid=ueuT_lH79uqMBM&vet=12ahUKEwi_9ISVx7aOAxXFcfEDHbCkIN0QM3oECFUQAA..i&w=506&h=495&hcb=2&ved=2ahUKEwi_9ISVx7aOAxXFcfEDHbCkIN0QM3oECFUQAA", false, "Björn Borg", "seed-user-id-123" }
                });
        }
    }
}
