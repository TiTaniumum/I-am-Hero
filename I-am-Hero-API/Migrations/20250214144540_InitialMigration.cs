using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace I_am_Hero_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cAttributeTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cAttributeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cCalendarBehaviours",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cCalendarBehaviours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cCalendarStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cCalendarStatuses", x => x.Id);
                });

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
                name: "cLevelCalculationTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cLevelCalculationTypes", x => x.Id);
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
                name: "cRarities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cRarities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Подтверждение пользователем свой почты."),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeroAchievements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cRarityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroAchievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroAchievements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroAchievements_cRarities_cRarityId",
                        column: x => x.cRarityId,
                        principalTable: "cRarities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HeroAttributes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cAttributeTypeId = table.Column<long>(type: "bigint", nullable: false),
                    MinValue = table.Column<long>(type: "bigint", nullable: true),
                    Value = table.Column<long>(type: "bigint", nullable: true),
                    MaxValue = table.Column<long>(type: "bigint", nullable: true),
                    CurrentStateId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroAttributes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroAttributes_cAttributeTypes_cAttributeTypeId",
                        column: x => x.cAttributeTypeId,
                        principalTable: "cAttributeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeroBioPieces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroBioPieces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroBioPieces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<long>(type: "bigint", nullable: false),
                    cLevelCalculationTypeId = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heroes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Heroes_cLevelCalculationTypes_cLevelCalculationTypeId",
                        column: x => x.cLevelCalculationTypeId,
                        principalTable: "cLevelCalculationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeroSkills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<long>(type: "bigint", nullable: false),
                    cLevelCalculationTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroSkills_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroSkills_cLevelCalculationTypes_cLevelCalculationTypeId",
                        column: x => x.cLevelCalculationTypeId,
                        principalTable: "cLevelCalculationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeroStatusEffects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroStatusEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroStatusEffects_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "Дата создания токена. Дефолт = GETDATE()"),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false, computedColumnSql: "DATEADD(DAY,14,[CreateDate])", comment: "Дата прекращения работы токена. Это вычисляемое значение от CreateDate. К нему прибавляется 14 дней."),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeroAttributeStates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeroAttributeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroAttributeStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeroAttributeStates_HeroAttributes_HeroAttributeId",
                        column: x => x.HeroAttributeId,
                        principalTable: "HeroAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Behaviours",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    HeroAttributeId = table.Column<long>(type: "bigint", nullable: true),
                    HeroSkillId = table.Column<long>(type: "bigint", nullable: true),
                    Sign = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Behaviours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Behaviours_HeroAttributes_HeroAttributeId",
                        column: x => x.HeroAttributeId,
                        principalTable: "HeroAttributes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Behaviours_HeroSkills_HeroSkillId",
                        column: x => x.HeroSkillId,
                        principalTable: "HeroSkills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Behaviours_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Дата начала действия календаря"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Дата окончания действия календаря"),
                    RewardBehaviourId = table.Column<long>(type: "bigint", nullable: true),
                    PenaltyBehaviourId = table.Column<long>(type: "bigint", nullable: true),
                    IgnoreBehaviourId = table.Column<long>(type: "bigint", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "Дата создания календаря. По умолчанию выставляется GETDATE()"),
                    StopDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Дата прекращения работы календаря. Пользователь не сможет ставть attendance. Выставляется когда пользователь больше не хочет пользоваться календарем."),
                    cCalendarBehaviourId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendars_Behaviours_IgnoreBehaviourId",
                        column: x => x.IgnoreBehaviourId,
                        principalTable: "Behaviours",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Calendars_Behaviours_PenaltyBehaviourId",
                        column: x => x.PenaltyBehaviourId,
                        principalTable: "Behaviours",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Calendars_Behaviours_RewardBehaviourId",
                        column: x => x.RewardBehaviourId,
                        principalTable: "Behaviours",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Calendars_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calendars_cCalendarBehaviours_cCalendarBehaviourId",
                        column: x => x.cCalendarBehaviourId,
                        principalTable: "cCalendarBehaviours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CompletionBehaviourId = table.Column<long>(type: "bigint", nullable: true),
                    FailureBehaviourId = table.Column<long>(type: "bigint", nullable: true),
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
                        name: "FK_QuestLines_Behaviours_CompletionBehaviourId",
                        column: x => x.CompletionBehaviourId,
                        principalTable: "Behaviours",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestLines_Behaviours_FailureBehaviourId",
                        column: x => x.FailureBehaviourId,
                        principalTable: "Behaviours",
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
                name: "CalendarAttendances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalendarId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    cCalendarStatusId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarAttendances_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarAttendances_cCalendarStatuses_cCalendarStatusId",
                        column: x => x.cCalendarStatusId,
                        principalTable: "cCalendarStatuses",
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
                    CompletionBehaviourId = table.Column<long>(type: "bigint", nullable: true),
                    FailureBehaviourId = table.Column<long>(type: "bigint", nullable: true),
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
                        name: "FK_Quests_Behaviours_CompletionBehaviourId",
                        column: x => x.CompletionBehaviourId,
                        principalTable: "Behaviours",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Quests_Behaviours_FailureBehaviourId",
                        column: x => x.FailureBehaviourId,
                        principalTable: "Behaviours",
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
                table: "Applications",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "I-am-Hero-Web" },
                    { 2L, "I-am-Hero-WPF" },
                    { 3L, "I-am-Hero-Android" },
                    { 4L, "I-am-Hero-IOS" }
                });

            migrationBuilder.InsertData(
                table: "cAttributeTypes",
                columns: new[] { "Id", "NameEn", "NameRu" },
                values: new object[,]
                {
                    { 1L, "Numerical", "Численный" },
                    { 2L, "State", "Состояние" }
                });

            migrationBuilder.InsertData(
                table: "cCalendarBehaviours",
                columns: new[] { "Id", "DescriptionEn", "DescriptionRu", "NameEn", "NameRu" },
                values: new object[,]
                {
                    { 1L, "On unmarked day, user will receive reward", "В день без отметки пользователь получит награду", "Positive", "Положительное" },
                    { 2L, "On unmarked day, user will receive penalty", "В день без отметки пользователь получит штраф", "Negative", "Отрицательное" },
                    { 3L, "On unmarked day, status will remain unmarked", "В день без отметки статус останется неотмеченным", "Neutral", "Нейтральное" }
                });

            migrationBuilder.InsertData(
                table: "cCalendarStatuses",
                columns: new[] { "Id", "NameEn", "NameRu" },
                values: new object[,]
                {
                    { 1L, "Marked", "Отмечено" },
                    { 2L, "Failed", "Провалено" },
                    { 3L, "Not marked", "Не отмечено" }
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
                table: "cLevelCalculationTypes",
                columns: new[] { "Id", "NameEn", "NameRu" },
                values: new object[,]
                {
                    { 1L, "Exponential", "Экспаненциальный" },
                    { 2L, "Constant", "Постоянный" },
                    { 3L, "Non-growing", "Нерастущий" }
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

            migrationBuilder.CreateIndex(
                name: "IX_Behaviours_HeroAttributeId",
                table: "Behaviours",
                column: "HeroAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Behaviours_HeroSkillId",
                table: "Behaviours",
                column: "HeroSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Behaviours_UserId",
                table: "Behaviours",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarAttendances_CalendarId",
                table: "CalendarAttendances",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarAttendances_cCalendarStatusId",
                table: "CalendarAttendances",
                column: "cCalendarStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_cCalendarBehaviourId",
                table: "Calendars",
                column: "cCalendarBehaviourId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_IgnoreBehaviourId",
                table: "Calendars",
                column: "IgnoreBehaviourId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_PenaltyBehaviourId",
                table: "Calendars",
                column: "PenaltyBehaviourId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_RewardBehaviourId",
                table: "Calendars",
                column: "RewardBehaviourId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_UserId",
                table: "Calendars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroAchievements_cRarityId",
                table: "HeroAchievements",
                column: "cRarityId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroAchievements_UserId",
                table: "HeroAchievements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroAttributes_cAttributeTypeId",
                table: "HeroAttributes",
                column: "cAttributeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroAttributes_UserId",
                table: "HeroAttributes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroAttributeStates_HeroAttributeId",
                table: "HeroAttributeStates",
                column: "HeroAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroBioPieces_UserId",
                table: "HeroBioPieces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_cLevelCalculationTypeId",
                table: "Heroes",
                column: "cLevelCalculationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_UserId",
                table: "Heroes",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HeroSkills_cLevelCalculationTypeId",
                table: "HeroSkills",
                column: "cLevelCalculationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroSkills_UserId",
                table: "HeroSkills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HeroStatusEffects_UserId",
                table: "HeroStatusEffects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLines_cDifficultyId",
                table: "QuestLines",
                column: "cDifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLines_CompletionBehaviourId",
                table: "QuestLines",
                column: "CompletionBehaviourId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLines_cQuestStatusId",
                table: "QuestLines",
                column: "cQuestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLines_FailureBehaviourId",
                table: "QuestLines",
                column: "FailureBehaviourId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestLines_UserId",
                table: "QuestLines",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_cDifficultyId",
                table: "Quests",
                column: "cDifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_CompletionBehaviourId",
                table: "Quests",
                column: "CompletionBehaviourId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_cQuestStatusId",
                table: "Quests",
                column: "cQuestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_FailureBehaviourId",
                table: "Quests",
                column: "FailureBehaviourId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_QuestLineId",
                table: "Quests",
                column: "QuestLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_UserId",
                table: "Quests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_ApplicationId",
                table: "Tokens",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarAttendances");

            migrationBuilder.DropTable(
                name: "HeroAchievements");

            migrationBuilder.DropTable(
                name: "HeroAttributeStates");

            migrationBuilder.DropTable(
                name: "HeroBioPieces");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "HeroStatusEffects");

            migrationBuilder.DropTable(
                name: "Quests");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "cCalendarStatuses");

            migrationBuilder.DropTable(
                name: "cRarities");

            migrationBuilder.DropTable(
                name: "QuestLines");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "cCalendarBehaviours");

            migrationBuilder.DropTable(
                name: "Behaviours");

            migrationBuilder.DropTable(
                name: "cDifficulties");

            migrationBuilder.DropTable(
                name: "cQuestStatuses");

            migrationBuilder.DropTable(
                name: "HeroAttributes");

            migrationBuilder.DropTable(
                name: "HeroSkills");

            migrationBuilder.DropTable(
                name: "cAttributeTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "cLevelCalculationTypes");
        }
    }
}
