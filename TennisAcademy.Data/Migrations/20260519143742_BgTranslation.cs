using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class BgTranslation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "22180021",
                table: "Surfaces",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Surface Name in English",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Surface Name");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "22180021",
                table: "Surfaces",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Image of the surface",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Ïmage of the surface");

            migrationBuilder.AddColumn<string>(
                name: "NameBg",
                schema: "22180021",
                table: "Surfaces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Surface Name in Bulgarian");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                schema: "22180021",
                table: "Rackets",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Racket Model in English",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Racket Model");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                schema: "22180021",
                table: "Rackets",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Racket Brand in English",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Racket Brand");

            migrationBuilder.AddColumn<string>(
                name: "BrandBg",
                schema: "22180021",
                table: "Rackets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Racket Brand in Bulgarian");

            migrationBuilder.AddColumn<string>(
                name: "ModelBg",
                schema: "22180021",
                table: "Rackets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Racket Model in Bulgarian");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                schema: "22180021",
                table: "Balls",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Ball Model in English",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Ball Model");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "22180021",
                table: "Balls",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Ball Image",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Racket Image");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                schema: "22180021",
                table: "Balls",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Ball Brand in English",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Ball Brand");

            migrationBuilder.AddColumn<string>(
                name: "BrandBg",
                schema: "22180021",
                table: "Balls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Ball Brand in Bulgarian");

            migrationBuilder.AddColumn<string>(
                name: "ModelBg",
                schema: "22180021",
                table: "Balls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Ball Model in Bulgarian");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                schema: "22180021",
                table: "Bags",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Bag Model in English",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Bag Model");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                schema: "22180021",
                table: "Bags",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Bag Brand in English",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Bag Brand");

            migrationBuilder.AddColumn<string>(
                name: "BrandBg",
                schema: "22180021",
                table: "Bags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Bag Brand in Bulgarian");

            migrationBuilder.AddColumn<string>(
                name: "ModelBg",
                schema: "22180021",
                table: "Bags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Bag Model in Bulgarian");

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Bags",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Уилсън", "Тийм 3-Пак" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Bags",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Хед", "Тур Тийм 6Р" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Bags",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Баболат", "Пюр Драйв Ер Ха х6" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Bags",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Йонекс", "Про Сериес 9-Пак" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Balls",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Уилсън", "Ю Ес Оупън Екстра Дюти" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Balls",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Хед", "Тур Екс Те" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Balls",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Дънлоп", "Ей Ти Пи Чемпиъншип" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Уилсън", "Про Стаф 97" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Баболат", "Пюр Драйв" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Хед", "Графен 360+ Спийд" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Йонекс", "Езоун 98" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Принс", "Тур 100П" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Rackets",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BrandBg", "ModelBg" },
                values: new object[] { "Технифайбър", "Т-Файт 305" });

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 1,
                column: "NameBg",
                value: "Клей (Червен корт)");

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 2,
                column: "NameBg",
                value: "Трева");

            migrationBuilder.UpdateData(
                schema: "22180021",
                table: "Surfaces",
                keyColumn: "Id",
                keyValue: 3,
                column: "NameBg",
                value: "Твърда настилка (Хард корт)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameBg",
                schema: "22180021",
                table: "Surfaces");

            migrationBuilder.DropColumn(
                name: "BrandBg",
                schema: "22180021",
                table: "Rackets");

            migrationBuilder.DropColumn(
                name: "ModelBg",
                schema: "22180021",
                table: "Rackets");

            migrationBuilder.DropColumn(
                name: "BrandBg",
                schema: "22180021",
                table: "Balls");

            migrationBuilder.DropColumn(
                name: "ModelBg",
                schema: "22180021",
                table: "Balls");

            migrationBuilder.DropColumn(
                name: "BrandBg",
                schema: "22180021",
                table: "Bags");

            migrationBuilder.DropColumn(
                name: "ModelBg",
                schema: "22180021",
                table: "Bags");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "22180021",
                table: "Surfaces",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Surface Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Surface Name in English");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "22180021",
                table: "Surfaces",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Ïmage of the surface",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Image of the surface");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                schema: "22180021",
                table: "Rackets",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Racket Model",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Racket Model in English");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                schema: "22180021",
                table: "Rackets",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Racket Brand",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Racket Brand in English");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                schema: "22180021",
                table: "Balls",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Ball Model",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Ball Model in English");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "22180021",
                table: "Balls",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Racket Image",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Ball Image");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                schema: "22180021",
                table: "Balls",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Ball Brand",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Ball Brand in English");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                schema: "22180021",
                table: "Bags",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Bag Model",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Bag Model in English");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                schema: "22180021",
                table: "Bags",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Bag Brand",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Bag Brand in English");
        }
    }
}
