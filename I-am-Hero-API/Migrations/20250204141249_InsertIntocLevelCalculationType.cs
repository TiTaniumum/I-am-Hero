using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace I_am_Hero_API.Migrations
{
    /// <inheritdoc />
    public partial class InsertIntocLevelCalculationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cAttributeTypes",
                columns: new[] { "Id", "NameEn", "NameRu" },
                values: new object[,]
                {
                    { 1L, "Numerical", "Численный" },
                    { 2L, "State", "Состояние" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cAttributeTypes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "cAttributeTypes",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
