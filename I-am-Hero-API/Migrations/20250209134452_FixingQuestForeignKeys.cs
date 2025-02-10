using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace I_am_Hero_API.Migrations
{
    /// <inheritdoc />
    public partial class FixingQuestForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestLines_QuestBehaviours_FailureQuestBehaviourId",
                table: "QuestLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Quests_QuestBehaviours_FailureQuestBehaviourId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_Quests_FailureQuestBehaviourId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_QuestLines_FailureQuestBehaviourId",
                table: "QuestLines");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_CompletionQuestBehaviourId",
                table: "Quests",
                column: "CompletionQuestBehaviourId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_FailureQuestBehaviourId",
                table: "Quests",
                column: "FailureQuestBehaviourId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLines_CompletionQuestBehaviourId",
                table: "QuestLines",
                column: "CompletionQuestBehaviourId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLines_FailureQuestBehaviourId",
                table: "QuestLines",
                column: "FailureQuestBehaviourId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestLines_QuestBehaviours_CompletionQuestBehaviourId",
                table: "QuestLines",
                column: "CompletionQuestBehaviourId",
                principalTable: "QuestBehaviours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestLines_QuestBehaviours_FailureQuestBehaviourId",
                table: "QuestLines",
                column: "FailureQuestBehaviourId",
                principalTable: "QuestBehaviours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_QuestBehaviours_CompletionQuestBehaviourId",
                table: "Quests",
                column: "CompletionQuestBehaviourId",
                principalTable: "QuestBehaviours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_QuestBehaviours_FailureQuestBehaviourId",
                table: "Quests",
                column: "FailureQuestBehaviourId",
                principalTable: "QuestBehaviours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestLines_QuestBehaviours_CompletionQuestBehaviourId",
                table: "QuestLines");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestLines_QuestBehaviours_FailureQuestBehaviourId",
                table: "QuestLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Quests_QuestBehaviours_CompletionQuestBehaviourId",
                table: "Quests");

            migrationBuilder.DropForeignKey(
                name: "FK_Quests_QuestBehaviours_FailureQuestBehaviourId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_Quests_CompletionQuestBehaviourId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_Quests_FailureQuestBehaviourId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_QuestLines_CompletionQuestBehaviourId",
                table: "QuestLines");

            migrationBuilder.DropIndex(
                name: "IX_QuestLines_FailureQuestBehaviourId",
                table: "QuestLines");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_FailureQuestBehaviourId",
                table: "Quests",
                column: "FailureQuestBehaviourId",
                unique: true,
                filter: "[FailureQuestBehaviourId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLines_FailureQuestBehaviourId",
                table: "QuestLines",
                column: "FailureQuestBehaviourId",
                unique: true,
                filter: "[FailureQuestBehaviourId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestLines_QuestBehaviours_FailureQuestBehaviourId",
                table: "QuestLines",
                column: "FailureQuestBehaviourId",
                principalTable: "QuestBehaviours",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_QuestBehaviours_FailureQuestBehaviourId",
                table: "Quests",
                column: "FailureQuestBehaviourId",
                principalTable: "QuestBehaviours",
                principalColumn: "Id");
        }
    }
}
