using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace I_am_Hero_API.Migrations
{
    /// <inheritdoc />
    public partial class InsertIntocRarity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cRarities",
                columns: new[] { "Id", "NameEn", "NameRu" },
                values: new object[,]
                {
                    { 1L, "Common", "Обычный" },
                    { 2L, "Uncommon", "Необычный" },
                    { 3L, "Unique", "Уникальный" },
                    { 4L, "Rare", "Редкий" },
                    { 5L, "Epic", "Эпический" },
                    { 6L, "Legendary", "Легендарный" },
                    { 7L, "Titanic", "Титанический" },
                    { 8L, "Mythical", "Мифический" },
                    { 9L, "Godlike", "Божественный" },
                    { 10L, "Secret", "Секретный" },
                    { 11L, "Unknown", "Неизвестный" },
                    { 12L, "Inconceivable", "Непостижимый" },
                    { 13L, "Inappreciable", "Недооцененный" },
                    { 14L, "Impossible", "Невозможный" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "cRarities",
                keyColumn: "Id",
                keyValue: 14L);
        }
    }
}
