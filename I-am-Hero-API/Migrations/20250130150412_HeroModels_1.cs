using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace I_am_Hero_API.Migrations
{
    /// <inheritdoc />
    public partial class HeroModels_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "ApplicationId",
                table: "Tokens",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<long>(type: "bigint", nullable: false),
                    cLevelCalculationTypeId = table.Column<long>(type: "bigint", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_ApplicationId",
                table: "Tokens",
                column: "ApplicationId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Applications_ApplicationId",
                table: "Tokens",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Applications_ApplicationId",
                table: "Tokens");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "HeroAchievements");

            migrationBuilder.DropTable(
                name: "HeroAttributeStates");

            migrationBuilder.DropTable(
                name: "HeroBioPieces");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "HeroSkills");

            migrationBuilder.DropTable(
                name: "HeroStatusEffects");

            migrationBuilder.DropTable(
                name: "cRarities");

            migrationBuilder.DropTable(
                name: "HeroAttributes");

            migrationBuilder.DropTable(
                name: "cLevelCalculationTypes");

            migrationBuilder.DropTable(
                name: "cAttributeTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_ApplicationId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Tokens");
        }
    }
}
