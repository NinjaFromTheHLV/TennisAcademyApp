using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class BgCoachTranslation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Coaches",
                schema: "22180021",
                oldComment: "Tennis Academy Coaches");

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                schema: "22180021",
                table: "Coaches",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Coach Nationality");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "22180021",
                table: "Coaches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Coach Name");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "22180021",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Coach Image");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "22180021",
                table: "Coaches",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldComment: "Coach Description");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                schema: "22180021",
                table: "Coaches",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Coach Age");

            migrationBuilder.AlterColumn<int>(
                name: "CoachId",
                schema: "22180021",
                table: "Coaches",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Coach Identifier")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionBg",
                schema: "22180021",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "22180021",
                table: "Coaches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NameBg",
                schema: "22180021",
                table: "Coaches",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NationalityBg",
                schema: "22180021",
                table: "Coaches",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 1,
                columns: new[] { "DescriptionBg", "IsDeleted", "NameBg", "NationalityBg" },
                values: new object[] { "Един от най-великите тенисисти на всички времена, известен с доминацията си на клей кортове.", false, "Рафаел Надал", "Испанец" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 2,
                columns: new[] { "DescriptionBg", "IsDeleted", "NameBg", "NationalityBg" },
                values: new object[] { "Швейцарска тенис легенда с ненадмината елегантност и 20 титли от Големия шлем.", false, "Роджър Федерер", "Швейцарец" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 3,
                columns: new[] { "DescriptionBg", "IsDeleted", "NameBg", "NationalityBg" },
                values: new object[] { "Сръбски шампион, известен със своята издръжливост и комплексна игра.", false, "Новак Джокович", "Сърбин" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 4,
                columns: new[] { "DescriptionBg", "IsDeleted", "NameBg", "NationalityBg" },
                values: new object[] { "Американска икона, която предефинира тениса през 90-те години с колоритна идентичност.", false, "Андре Агаси", "Американец" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Coaches",
                keyColumn: "CoachId",
                keyValue: 5,
                columns: new[] { "DescriptionBg", "IsDeleted", "NameBg", "NationalityBg" },
                values: new object[] { "Шведска легенда с ледени нерви и шест титли от Ролан Гарос.", false, "Бьорн Борг", "Швед" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionBg",
                schema: "22180021",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "22180021",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "NameBg",
                schema: "22180021",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "NationalityBg",
                schema: "22180021",
                table: "Coaches");

            migrationBuilder.AlterTable(
                name: "Coaches",
                schema: "22180021",
                comment: "Tennis Academy Coaches");

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                schema: "22180021",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Coach Nationality",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "22180021",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Coach Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "22180021",
                table: "Coaches",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Coach Image",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "22180021",
                table: "Coaches",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                comment: "Coach Description",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                schema: "22180021",
                table: "Coaches",
                type: "int",
                nullable: false,
                comment: "Coach Age",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CoachId",
                schema: "22180021",
                table: "Coaches",
                type: "int",
                nullable: false,
                comment: "Coach Identifier",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
