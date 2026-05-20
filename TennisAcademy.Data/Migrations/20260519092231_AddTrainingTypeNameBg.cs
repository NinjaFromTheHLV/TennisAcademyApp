using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingTypeNameBg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "22180021",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Training Type Name in English",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Training Type Name");

            migrationBuilder.AddColumn<string>(
                name: "NameBg",
                schema: "22180021",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Training Type Name in Bulgarian");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                schema: "22180021",
                table: "Reservations",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true,
                comment: "Player Notes in English",
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldNullable: true,
                oldComment: "Player Notes");

            migrationBuilder.AddColumn<string>(
                name: "NoteBg",
                schema: "22180021",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Player Notes in Bulgarian");

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 1,
                column: "NameBg",
                value: "Физическа подготовка");

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 2,
                column: "NameBg",
                value: "Развитие на технически умения");

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 3,
                column: "NameBg",
                value: "Тактическа стратегия за игра");

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Trainings",
                keyColumn: "Id",
                keyValue: 4,
                column: "NameBg",
                value: "Психологическа устойчивост и ментална тренировка");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameBg",
                schema: "22180021",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "NoteBg",
                schema: "22180021",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "22180021",
                table: "Trainings",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Training Type Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Training Type Name in English");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                schema: "22180021",
                table: "Reservations",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true,
                comment: "Player Notes",
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldNullable: true,
                oldComment: "Player Notes in English");
        }
    }
}
