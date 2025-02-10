using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace I_am_Hero_API.Migrations
{
    /// <inheritdoc />
    public partial class FixingQuestForeignKeys2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "QuestBehaviours",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_QuestBehaviours_UserId",
                table: "QuestBehaviours",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestBehaviours_Users_UserId",
                table: "QuestBehaviours",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestLines_QuestBehaviours_CompletionQuestBehaviourId",
                table: "QuestLines",
                column: "CompletionQuestBehaviourId",
                principalTable: "QuestBehaviours",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestLines_QuestBehaviours_FailureQuestBehaviourId",
                table: "QuestLines",
                column: "FailureQuestBehaviourId",
                principalTable: "QuestBehaviours",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_QuestBehaviours_CompletionQuestBehaviourId",
                table: "Quests",
                column: "CompletionQuestBehaviourId",
                principalTable: "QuestBehaviours",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_QuestBehaviours_FailureQuestBehaviourId",
                table: "Quests",
                column: "FailureQuestBehaviourId",
                principalTable: "QuestBehaviours",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestBehaviours_Users_UserId",
                table: "QuestBehaviours");

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
                name: "IX_QuestBehaviours_UserId",
                table: "QuestBehaviours");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "QuestBehaviours");

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
    }
}
