using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TennisAcademyApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoreTournaments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "22180021",
                table: "TournamentCategories",
                columns: new[] { "Id", "IsDeleted", "Name", "NameBg" },
                values: new object[,]
                {
                    { 4, false, "Doubles Mixed", "Смесени Двойки" },
                    { 5, false, "Veterans 45+", "Ветерани 45+" },
                    { 6, false, "Amateur League", "Аматьорска Лига" }
                });

            migrationBuilder.InsertData(
                schema: "22180021",
                table: "Tournaments",
                columns: new[] { "Id", "CategoryId", "Description", "DescriptionBg", "EndDate", "EntryFee", "IsDeleted", "MaxParticipants", "StartDate", "Title", "TitleBg" },
                values: new object[,]
                {
                    { 7, 1, "Experience the thrill of playing under the lights. Evening matches on fast hard courts.", "Изживейте тръпката от играта под светлините на прожекторите. Вечерни мачове на бързи твърди кортове.", new DateTime(2026, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 55.00m, false, 32, new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "August Night Hardcourt Championship", "Августовски Нощен Шампионат" },
                    { 8, 2, "Gathering the best local female players for an end-of-season showdown on clay.", "Събиране на най-добрите местни тенисистки за сблъсък в края на сезона на клей корт.", new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 35.00m, false, 16, new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Autumn Women Single Open", "Есенен Отворен Шампионат за Жени" },
                    { 9, 3, "An exciting singles tournament for juniors to celebrate the new school season. Lots of prizes.", "Вълнуващ сингъл турнир за юноши по случай новия учебен сезон. Множество награди.", new DateTime(2026, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.00m, false, 32, new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Back to School Youth Cup", "Младежка Купа 'Обратно на Училище'" },
                    { 11, 1, "The first grand tournament of the winter season inside the academy’s premium heated halls.", "Първият голям турнир за зимния сезон вътре в премиум отопляемите зали на академията.", new DateTime(2026, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.00m, false, 32, new DateTime(2026, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indoor Premium Cup", "Закрит Премиум Шампионат" },
                    { 4, 4, "Bring your partner and fight for the grand trophy. Fun and highly competitive atmosphere.", "Доведете партньора си и се борете за голямия трофей. Забавна и силно конкурентна атмосфера.", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.00m, false, 16, new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Midsummer Mixed Doubles", "Летни Смесени Двойки" },
                    { 5, 5, "Exclusively for players aged 45 and above. Hard court battles, tactical play, and great experience.", "Ексклузивно за играчи на възраст 45 и повече години. Битки на твърди кортове, тактическа игра и страхотно изживяване.", new DateTime(2026, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 45.00m, false, 32, new DateTime(2026, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masters Veterans Tournament", "Мастърс Турнир за Ветерани" },
                    { 6, 6, "Perfect tournament for recreation players who want to try competitive tennis. Matches played after 18:00.", "Перфектен турнир за любители, които искат да се пробват в състезателния тенис. Мачовете се играят след 18:00 часа.", new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.00m, false, 64, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Weekend Warrior Amateur League", "Лига 'Уикенд Воини' за Аматьори" },
                    { 10, 4, "The ultimate team tournament before moving to indoor courts. Group phase followed by eliminations.", "Финалният отборен турнир преди преместването в закрити кортове. Групова фаза, последвана от елиминации.", new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.00m, false, 16, new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Golden Autumn Doubles", "Златна Есен Смесени Двойки" },
                    { 12, 5, "Winter edition of our highly anticipated veteran tournament. Keep the competitive spirit alive.", "Зимно издание на нашия дългоочакван ветерански турнир. Поддържайте състезателния дух жив.", new DateTime(2026, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 45.00m, false, 16, new DateTime(2026, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Legends 45+ Winter Trophy", "Зимна Трофейна Лига за Легенди 45+" },
                    { 13, 6, "Our final event of the year. All entry fees will be donated to local youth sports development.", "Последното ни събитие за годината. Всички такси за участие ще бъдат дарени за развитието на местния младежки спорт.", new DateTime(2026, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 40.00m, false, 64, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christmas Academy Charity Slams", "Коледен Благотворителен Шлем на Академията" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "TournamentCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "TournamentCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "22180021",
                table: "TournamentCategories",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
