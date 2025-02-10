﻿// <auto-generated />
using System;
using I_am_Hero_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace I_am_Hero_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250209125010_CreateQuestsAndQuestLines")]
    partial class CreateQuestsAndQuestLines
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("I_am_Hero_API.Models.Application", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Applications");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "I-am-Hero-Web"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "I-am-Hero-WPF"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "I-am-Hero-Android"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "I-am-Hero-IOS"
                        });
                });

            modelBuilder.Entity("I_am_Hero_API.Models.Hero", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("Experience")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("cLevelCalculationTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(1L);

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasIndex("cLevelCalculationTypeId");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroAchievement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("cRarityId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("cRarityId");

                    b.ToTable("HeroAchievements");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroAttribute", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("CurrentStateId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("MaxValue")
                        .HasColumnType("bigint");

                    b.Property<long?>("MinValue")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("Value")
                        .HasColumnType("bigint");

                    b.Property<long>("cAttributeTypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("cAttributeTypeId");

                    b.ToTable("HeroAttributes");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroAttributeState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("HeroAttributeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HeroAttributeId");

                    b.ToTable("HeroAttributeStates");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroBioPiece", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("HeroBioPieces");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroSkill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Experience")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("cLevelCalculationTypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("cLevelCalculationTypeId");

                    b.ToTable("HeroSkills");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroStatusEffect", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("Value")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("HeroStatusEffects");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.Quest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("ArchiveDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CompletionQuestBehaviourId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Experinece")
                        .HasColumnType("bigint");

                    b.Property<long?>("FailureQuestBehaviourId")
                        .HasColumnType("bigint");

                    b.Property<int?>("Priority")
                        .HasColumnType("int");

                    b.Property<long?>("QuestLineId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("cDifficultyId")
                        .HasColumnType("bigint");

                    b.Property<long?>("cQuestStatusId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FailureQuestBehaviourId")
                        .IsUnique()
                        .HasFilter("[FailureQuestBehaviourId] IS NOT NULL");

                    b.HasIndex("QuestLineId");

                    b.HasIndex("UserId");

                    b.HasIndex("cDifficultyId");

                    b.HasIndex("cQuestStatusId");

                    b.ToTable("Quests");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.QuestBehaviour", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("HeroAttirbuteId")
                        .HasColumnType("bigint");

                    b.Property<long?>("HeroAttributeId")
                        .HasColumnType("bigint");

                    b.Property<long?>("HeroSkillId")
                        .HasColumnType("bigint");

                    b.Property<string>("Sign")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Value")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("HeroAttributeId");

                    b.HasIndex("HeroSkillId");

                    b.ToTable("QuestBehaviours");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.QuestLine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("ArchiveDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CompletionQuestBehaviourId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Experinece")
                        .HasColumnType("bigint");

                    b.Property<long?>("FailureQuestBehaviourId")
                        .HasColumnType("bigint");

                    b.Property<int?>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("cDifficultyId")
                        .HasColumnType("bigint");

                    b.Property<long?>("cQuestStatusId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FailureQuestBehaviourId")
                        .IsUnique()
                        .HasFilter("[FailureQuestBehaviourId] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.HasIndex("cDifficultyId");

                    b.HasIndex("cQuestStatusId");

                    b.ToTable("QuestLines");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.Token", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ApplicationId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()")
                        .HasComment("Дата создания токена. Дефолт = GETDATE()");

                    b.Property<DateTime>("ExpireDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasComputedColumnSql("DATEADD(DAY,14,[CreateDate])")
                        .HasComment("Дата прекращения работы токена. Это вычисляемое значение от CreateDate. К нему прибавляется 14 дней.");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Token");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasComment("Подтверждение пользователем свой почты.");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.cAttributeType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cAttributeTypes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            NameEn = "Numerical",
                            NameRu = "Численный"
                        },
                        new
                        {
                            Id = 2L,
                            NameEn = "State",
                            NameRu = "Состояние"
                        });
                });

            modelBuilder.Entity("I_am_Hero_API.Models.cDifficulty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cDifficulties");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            NameEn = "Easy",
                            NameRu = "Легко"
                        },
                        new
                        {
                            Id = 2L,
                            NameEn = "Normal",
                            NameRu = "Нормально"
                        },
                        new
                        {
                            Id = 3L,
                            NameEn = "Hard",
                            NameRu = "Сложно"
                        },
                        new
                        {
                            Id = 4L,
                            NameEn = "Very hard",
                            NameRu = "Очень сложно"
                        },
                        new
                        {
                            Id = 5L,
                            NameEn = "Impossible",
                            NameRu = "Невозможно"
                        },
                        new
                        {
                            Id = 6L,
                            NameEn = "Lifegoal",
                            NameRu = "Жизненная цель"
                        },
                        new
                        {
                            Id = 7L,
                            NameEn = "Life-threatening",
                            NameRu = "Угрожающий жизни"
                        });
                });

            modelBuilder.Entity("I_am_Hero_API.Models.cLevelCalculationType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cLevelCalculationTypes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            NameEn = "Exponential",
                            NameRu = "Экспаненциальный"
                        },
                        new
                        {
                            Id = 2L,
                            NameEn = "Constant",
                            NameRu = "Постоянный"
                        },
                        new
                        {
                            Id = 3L,
                            NameEn = "Non-growing",
                            NameRu = "Нерастущий"
                        });
                });

            modelBuilder.Entity("I_am_Hero_API.Models.cQuestStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cQuestStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            NameEn = "Not started",
                            NameRu = "Не начато"
                        },
                        new
                        {
                            Id = 2L,
                            NameEn = "Active",
                            NameRu = "Активно"
                        },
                        new
                        {
                            Id = 3L,
                            NameEn = "Completed",
                            NameRu = "Завершено"
                        },
                        new
                        {
                            Id = 4L,
                            NameEn = "Failed",
                            NameRu = "Провалено"
                        },
                        new
                        {
                            Id = 5L,
                            NameEn = "Canceled",
                            NameRu = "Отменено"
                        },
                        new
                        {
                            Id = 6L,
                            NameEn = "Archived",
                            NameRu = "Архивировано"
                        },
                        new
                        {
                            Id = 7L,
                            NameEn = "Deleted",
                            NameRu = "Удалено"
                        },
                        new
                        {
                            Id = 8L,
                            NameEn = "Abandoned",
                            NameRu = "Покинуто"
                        },
                        new
                        {
                            Id = 9L,
                            NameEn = "Paused",
                            NameRu = "Приостановлено"
                        },
                        new
                        {
                            Id = 10L,
                            NameEn = "Hidden",
                            NameRu = "Скрыто"
                        });
                });

            modelBuilder.Entity("I_am_Hero_API.Models.cRarity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("cRarities");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            NameEn = "Common",
                            NameRu = "Обычный"
                        },
                        new
                        {
                            Id = 2L,
                            NameEn = "Uncommon",
                            NameRu = "Необычный"
                        },
                        new
                        {
                            Id = 3L,
                            NameEn = "Unique",
                            NameRu = "Уникальный"
                        },
                        new
                        {
                            Id = 4L,
                            NameEn = "Rare",
                            NameRu = "Редкий"
                        },
                        new
                        {
                            Id = 5L,
                            NameEn = "Epic",
                            NameRu = "Эпический"
                        },
                        new
                        {
                            Id = 6L,
                            NameEn = "Legendary",
                            NameRu = "Легендарный"
                        },
                        new
                        {
                            Id = 7L,
                            NameEn = "Titanic",
                            NameRu = "Титанический"
                        },
                        new
                        {
                            Id = 8L,
                            NameEn = "Mythical",
                            NameRu = "Мифический"
                        },
                        new
                        {
                            Id = 9L,
                            NameEn = "Godlike",
                            NameRu = "Божественный"
                        },
                        new
                        {
                            Id = 10L,
                            NameEn = "Secret",
                            NameRu = "Секретный"
                        },
                        new
                        {
                            Id = 11L,
                            NameEn = "Unknown",
                            NameRu = "Неизвестный"
                        },
                        new
                        {
                            Id = 12L,
                            NameEn = "Inconceivable",
                            NameRu = "Непостижимый"
                        },
                        new
                        {
                            Id = 13L,
                            NameEn = "Inappreciable",
                            NameRu = "Недооцененный"
                        },
                        new
                        {
                            Id = 14L,
                            NameEn = "Impossible",
                            NameRu = "Невозможный"
                        });
                });

            modelBuilder.Entity("I_am_Hero_API.Models.Hero", b =>
                {
                    b.HasOne("I_am_Hero_API.Models.User", "User")
                        .WithOne("Hero")
                        .HasForeignKey("I_am_Hero_API.Models.Hero", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("I_am_Hero_API.Models.cLevelCalculationType", "cLevelCalculationType")
                        .WithMany()
                        .HasForeignKey("cLevelCalculationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("cLevelCalculationType");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroAchievement", b =>
                {
                    b.HasOne("I_am_Hero_API.Models.User", "User")
                        .WithMany("HeroAchievements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("I_am_Hero_API.Models.cRarity", "cRarity")
                        .WithMany()
                        .HasForeignKey("cRarityId");

                    b.Navigation("User");

                    b.Navigation("cRarity");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroAttribute", b =>
                {
                    b.HasOne("I_am_Hero_API.Models.User", "User")
                        .WithMany("HeroAttributes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("I_am_Hero_API.Models.cAttributeType", "cAttributeType")
                        .WithMany()
                        .HasForeignKey("cAttributeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("cAttributeType");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroAttributeState", b =>
                {
                    b.HasOne("I_am_Hero_API.Models.HeroAttribute", "HeroAttribute")
                        .WithMany("HeroAttributeStates")
                        .HasForeignKey("HeroAttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HeroAttribute");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroBioPiece", b =>
                {
                    b.HasOne("I_am_Hero_API.Models.User", "User")
                        .WithMany("HeroBioPieces")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroSkill", b =>
                {
                    b.HasOne("I_am_Hero_API.Models.User", "User")
                        .WithMany("HeroSkills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("I_am_Hero_API.Models.cLevelCalculationType", "cLevelCalculationType")
                        .WithMany()
                        .HasForeignKey("cLevelCalculationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("cLevelCalculationType");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroStatusEffect", b =>
                {
                    b.HasOne("I_am_Hero_API.Models.User", "User")
                        .WithMany("HeroStatusEffects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.Quest", b =>
                {
                    b.HasOne("I_am_Hero_API.Models.QuestBehaviour", null)
                        .WithOne("Quest")
                        .HasForeignKey("I_am_Hero_API.Models.Quest", "FailureQuestBehaviourId");

                    b.HasOne("I_am_Hero_API.Models.QuestLine", "QuestLine")
                        .WithMany("Quests")
                        .HasForeignKey("QuestLineId");

                    b.HasOne("I_am_Hero_API.Models.User", "User")
                        .WithMany("Quests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("I_am_Hero_API.Models.cDifficulty", "cDifficulty")
                        .WithMany()
                        .HasForeignKey("cDifficultyId");

                    b.HasOne("I_am_Hero_API.Models.cQuestStatus", "cQuestStatus")
                        .WithMany()
                        .HasForeignKey("cQuestStatusId");

                    b.Navigation("QuestLine");

                    b.Navigation("User");

                    b.Navigation("cDifficulty");

                    b.Navigation("cQuestStatus");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.QuestBehaviour", b =>
                {
                    b.HasOne("I_am_Hero_API.Models.HeroAttribute", "HeroAttribute")
                        .WithMany()
                        .HasForeignKey("HeroAttributeId");

                    b.HasOne("I_am_Hero_API.Models.HeroSkill", "HeroSkill")
                        .WithMany()
                        .HasForeignKey("HeroSkillId");

                    b.Navigation("HeroAttribute");

                    b.Navigation("HeroSkill");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.QuestLine", b =>
                {
                    b.HasOne("I_am_Hero_API.Models.QuestBehaviour", null)
                        .WithOne("QuestLine")
                        .HasForeignKey("I_am_Hero_API.Models.QuestLine", "FailureQuestBehaviourId");

                    b.HasOne("I_am_Hero_API.Models.User", "User")
                        .WithMany("QuestsLines")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("I_am_Hero_API.Models.cDifficulty", "cDifficulty")
                        .WithMany()
                        .HasForeignKey("cDifficultyId");

                    b.HasOne("I_am_Hero_API.Models.cQuestStatus", "cQuestStatus")
                        .WithMany()
                        .HasForeignKey("cQuestStatusId");

                    b.Navigation("User");

                    b.Navigation("cDifficulty");

                    b.Navigation("cQuestStatus");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.Token", b =>
                {
                    b.HasOne("I_am_Hero_API.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("I_am_Hero_API.Models.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("User");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.HeroAttribute", b =>
                {
                    b.Navigation("HeroAttributeStates");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.QuestBehaviour", b =>
                {
                    b.Navigation("Quest");

                    b.Navigation("QuestLine");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.QuestLine", b =>
                {
                    b.Navigation("Quests");
                });

            modelBuilder.Entity("I_am_Hero_API.Models.User", b =>
                {
                    b.Navigation("Hero");

                    b.Navigation("HeroAchievements");

                    b.Navigation("HeroAttributes");

                    b.Navigation("HeroBioPieces");

                    b.Navigation("HeroSkills");

                    b.Navigation("HeroStatusEffects");

                    b.Navigation("Quests");

                    b.Navigation("QuestsLines");

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
