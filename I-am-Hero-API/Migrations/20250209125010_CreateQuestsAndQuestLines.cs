using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace I_am_Hero_API.Migrations
{
    /// <inheritdoc />
    public partial class CreateQuestsAndQuestLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cDifficulties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameRu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cDifficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cQuestStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameRu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cQuestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestBehaviours",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeroAttirbuteId = table.Column<long>(type: "bigint", nullable: true),
                    HeroAttributeId = table.Column<long>(type: "bigint", nullable: true),
                    HeroSkillId = table.Column<long>(type: "bigint", nullable: true),
                    Sign = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestBehaviours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestBehaviours_HeroAttributes_HeroAttributeId",
                        column: x => x.HeroAttributeId,
                        principalTable: "HeroAttributes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestBehaviours_HeroSkills_HeroSkillId",
                        column: x => x.HeroSkillId,
                        principalTable: "HeroSkills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestLines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experinece = table.Column<long>(type: "bigint", nullable: false),
                    CompletionQuestBehaviourId = table.Column<long>(type: "bigint", nullable: true),
                    FailureQuestBehaviourId = table.Column<long>(type: "bigint", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    cDifficultyId = table.Column<long>(type: "bigint", nullable: true),
                    cQuestStatusId = table.Column<long>(type: "bigint", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestLines_QuestBehaviours_FailureQuestBehaviourId",
                        column: x => x.FailureQuestBehaviourId,
                        principalTable: "QuestBehaviours",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestLines_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestLines_cDifficulties_cDifficultyId",
                        column: x => x.cDifficultyId,
                        principalTable: "cDifficulties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestLines_cQuestStatuses_cQuestStatusId",
                        column: x => x.cQuestStatusId,
                        principalTable: "cQuestStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experinece = table.Column<long>(type: "bigint", nullable: false),
                    CompletionQuestBehaviourId = table.Column<long>(type: "bigint", nullable: true),
                    FailureQuestBehaviourId = table.Column<long>(type: "bigint", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    cDifficultyId = table.Column<long>(type: "bigint", nullable: true),
                    cQuestStatusId = table.Column<long>(type: "bigint", nullable: true),
                    QuestLineId = table.Column<long>(type: "bigint", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ArchiveDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quests_QuestBehaviours_FailureQuestBehaviourId",
                        column: x => x.FailureQuestBehaviourId,
                        principalTable: "QuestBehaviours",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Quests_QuestLines_QuestLineId",
                        column: x => x.QuestLineId,
                        principalTable: "QuestLines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Quests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quests_cDifficulties_cDifficultyId",
                        column: x => x.cDifficultyId,
                        principalTable: "cDifficulties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Quests_cQuestStatuses_cQuestStatusId",
                        column: x => x.cQuestStatusId,
                        principalTable: "cQuestStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "cDifficulties",
                columns: new[] { "Id", "NameEn", "NameRu" },
                values: new object[,]
                {
                    { 1L, "Easy", "Легко" },
                    { 2L, "Normal", "Нормально" },
                    { 3L, "Hard", "Сложно" },
                    { 4L, "Very hard", "Очень сложно" },
                    { 5L, "Impossible", "Невозможно" },
                    { 6L, "Lifegoal", "Жизненная цель" },
                    { 7L, "Life-threatening", "Угрожающий жизни" }
                });

            migrationBuilder.InsertData(
                table: "cQuestStatuses",
                columns: new[] { "Id", "NameEn", "NameRu" },
                values: new object[,]
                {
                    { 1L, "Not started", "Не начато" },
                    { 2L, "Active", "Активно" },
                    { 3L, "Completed", "Завершено" },
                    { 4L, "Failed", "Провалено" },
                    { 5L, "Canceled", "Отменено" },
                    { 6L, "Archived", "Архивировано" },
                    { 7L, "Deleted", "Удалено" },
                    { 8L, "Abandoned", "Покинуто" },
                    { 9L, "Paused", "Приостановлено" },
                    { 10L, "Hidden", "Скрыто" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestBehaviours_HeroAttributeId",
                table: "QuestBehaviours",
                column: "HeroAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestBehaviours_HeroSkillId",
                table: "QuestBehaviours",
                column: "HeroSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLines_cDifficultyId",
                table: "QuestLines",
                column: "cDifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLines_cQuestStatusId",
                table: "QuestLines",
                column: "cQuestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLines_FailureQuestBehaviourId",
                table: "QuestLines",
                column: "FailureQuestBehaviourId",
                unique: true,
                filter: "[FailureQuestBehaviourId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLines_UserId",
                table: "QuestLines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_cDifficultyId",
                table: "Quests",
                column: "cDifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_cQuestStatusId",
                table: "Quests",
                column: "cQuestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_FailureQuestBehaviourId",
                table: "Quests",
                column: "FailureQuestBehaviourId",
                unique: true,
                filter: "[FailureQuestBehaviourId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_QuestLineId",
                table: "Quests",
                column: "QuestLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_UserId",
                table: "Quests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quests");

            migrationBuilder.DropTable(
                name: "QuestLines");

            migrationBuilder.DropTable(
                name: "QuestBehaviours");

            migrationBuilder.DropTable(
                name: "cDifficulties");

            migrationBuilder.DropTable(
                name: "cQuestStatuses");
        }
    }
}
