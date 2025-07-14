using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeededSurfacesAndTrainings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id-123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eaf473c9-4360-4fee-953d-9e3640817b32", "AQAAAAIAAYagAAAAELAChf5L0uFAPYNNA48OVs1jcdkVZG6ksA3wpE8nyHToDX4zg2FWy/NIg57FAAgc+Q==", "83d3d211-7d23-4389-a5f3-0652ce6d0391" });

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
    }
}
