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
    [Migration("20250130150412_HeroModels_1")]
    partial class HeroModels_1
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
                        .HasColumnType("bigint");

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
                        .HasColumnType("datetime2");

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

            modelBuilder.Entity("I_am_Hero_API.Models.User", b =>
                {
                    b.Navigation("Hero");

                    b.Navigation("HeroAchievements");

                    b.Navigation("HeroAttributes");

                    b.Navigation("HeroBioPieces");

                    b.Navigation("HeroSkills");

                    b.Navigation("HeroStatusEffects");

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
