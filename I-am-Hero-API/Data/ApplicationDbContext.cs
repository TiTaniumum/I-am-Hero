﻿using I_am_Hero_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace I_am_Hero_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<HeroSkill> HeroSkills { get; set; }
        public DbSet<cLevelCalculationType> cLevelCalculationTypes { get; set; }
        public DbSet<HeroAttribute> HeroAttributes { get; set; }
        public DbSet<cAttributeType> cAttributeTypes { get; set; }
        public DbSet<HeroAttributeState> HeroAttributeStates { get; set; }
        public DbSet<HeroStatusEffect> HeroStatusEffects { get; set; }
        public DbSet<HeroBioPiece> HeroBioPieces { get; set; }
        public DbSet<HeroAchievement> HeroAchievements { get; set; }
        public DbSet<cRarity> cRarities { get; set; }
        public DbSet<cQuestStatus> cQuestStatuses { get; set; }
        public DbSet<cDifficulty> cDifficulties { get; set; }
        public DbSet<Behaviour> Behaviours { get; set; }
        public DbSet<QuestLine> QuestLines { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<cCalendarBehaviour> cCalendarBehaviours { get; set; }
        public DbSet<cCalendarStatus> cCalendarStatuses { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<CalendarAttendance> CalendarAttendances { get; set; }
        public DbSet<cPopupInterval> cPopupIntervals { get; set; }
        public DbSet<HeroHabbit> HeroHabbits { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.IsEmailVerified)
                .HasDefaultValue(false);
            modelBuilder.Entity<User>()
                .Property(x => x.RegistrationDate)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Token>()
                .Property(x => x.CreateDate)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Token>()
                .Property(x => x.ExpireDate)
                .HasComputedColumnSql("DATEADD(DAY,14,[CreateDate])");
            modelBuilder.Entity<HeroBioPiece>()
                .Property(x => x.CreateDate)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Application>()
                .HasData(
                    new Application { Id = 1, Name = "I-am-Hero-Web" },
                    new Application { Id = 2, Name = "I-am-Hero-WPF" },
                    new Application { Id = 3, Name = "I-am-Hero-Android" },
                    new Application { Id = 4, Name = "I-am-Hero-IOS" }
                );
            modelBuilder.Entity<cLevelCalculationType>()
                .HasData(
                    new cLevelCalculationType { Id = 1, NameRu = "Экспаненциальный", NameEn = "Exponential" },
                    new cLevelCalculationType { Id = 2, NameRu = "Постоянный", NameEn = "Constant" },
                    new cLevelCalculationType { Id = 3, NameRu = "Нерастущий", NameEn = "Non-growing" }
                );
            modelBuilder.Entity<Hero>()
                .Property(x => x.cLevelCalculationTypeId)
                .HasDefaultValue(1);
            modelBuilder.Entity<cAttributeType>()
                .HasData(
                    new cAttributeType { Id = 1, NameEn = "Numerical", NameRu = "Численный" },
                    new cAttributeType { Id = 2, NameEn = "State", NameRu = "Состояние" }
                );
            modelBuilder.Entity<cRarity>()
                .HasData(
                    new cRarity { Id = 1, NameEn = "Common", NameRu = "Обычный" },
                    new cRarity { Id = 2, NameEn = "Uncommon", NameRu = "Необычный" },
                    new cRarity { Id = 3, NameEn = "Unique", NameRu = "Уникальный" },
                    new cRarity { Id = 4, NameEn = "Rare", NameRu = "Редкий" },
                    new cRarity { Id = 5, NameEn = "Epic", NameRu = "Эпический" },
                    new cRarity { Id = 6, NameEn = "Legendary", NameRu = "Легендарный" },
                    new cRarity { Id = 7, NameEn = "Titanic", NameRu = "Титанический" },
                    new cRarity { Id = 8, NameEn = "Mythical", NameRu = "Мифический" },
                    new cRarity { Id = 9, NameEn = "Godlike", NameRu = "Божественный" },
                    new cRarity { Id = 10, NameEn = "Secret", NameRu = "Секретный" },
                    new cRarity { Id = 11, NameEn = "Unknown", NameRu = "Неизвестный" },
                    new cRarity { Id = 12, NameEn = "Inconceivable", NameRu = "Непостижимый" },
                    new cRarity { Id = 13, NameEn = "Inappreciable", NameRu = "Недооцененный" },
                    new cRarity { Id = 14, NameEn = "Impossible", NameRu = "Невозможный" }
                );
            modelBuilder.Entity<QuestLine>()
                .Property(x => x.CreateDate)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Quest>()
                .Property(x => x.CreateDate)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<cDifficulty>()
                .HasData(
                    new cDifficulty { Id = 1, NameEn = "Easy", NameRu = "Легко" },
                    new cDifficulty { Id = 2, NameEn = "Normal", NameRu = "Нормально" },
                    new cDifficulty { Id = 3, NameEn = "Hard", NameRu = "Сложно" },
                    new cDifficulty { Id = 4, NameEn = "Very hard", NameRu = "Очень сложно" },
                    new cDifficulty { Id = 5, NameEn = "Impossible", NameRu = "Невозможно" },
                    new cDifficulty { Id = 6, NameEn = "Lifegoal", NameRu = "Жизненная цель" },
                    new cDifficulty { Id = 7, NameEn = "Life-threatening", NameRu = "Угрожающий жизни" }
                );
            modelBuilder.Entity<cQuestStatus>()
                .HasData(
                    new cQuestStatus { Id = 1, NameEn = "Not started", NameRu = "Не начато" },
                    new cQuestStatus { Id = 2, NameEn = "Active", NameRu = "Активно" },
                    new cQuestStatus { Id = 3, NameEn = "Completed", NameRu = "Завершено" },
                    new cQuestStatus { Id = 4, NameEn = "Failed", NameRu = "Провалено" },
                    new cQuestStatus { Id = 5, NameEn = "Canceled", NameRu = "Отменено" },
                    new cQuestStatus { Id = 6, NameEn = "Archived", NameRu = "Архивировано" },
                    new cQuestStatus { Id = 7, NameEn = "Deleted", NameRu = "Удалено" },
                    new cQuestStatus { Id = 8, NameEn = "Abandoned", NameRu = "Покинуто" },
                    new cQuestStatus { Id = 9, NameEn = "Paused", NameRu = "Приостановлено" },
                    new cQuestStatus { Id = 10, NameEn = "Hidden", NameRu = "Скрыто" }
                );
            modelBuilder.Entity<QuestLine>()
                .HasOne(q => q.CompletionBehaviour)
                .WithMany()
                .HasForeignKey(q => q.CompletionBehaviourId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<QuestLine>()
                .HasOne(q => q.FailureBehaviour)
                .WithMany()
                .HasForeignKey(q => q.FailureBehaviourId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Quest>()
                .HasOne(q => q.CompletionBehaviour)
                .WithMany()
                .HasForeignKey(q => q.CompletionBehaviourId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Quest>()
                .HasOne(q => q.FailureBehaviour)
                .WithMany()
                .HasForeignKey(q => q.FailureBehaviourId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Calendar>()
                .Property(x => x.CreateDate)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Calendar>()
                .HasOne(q => q.RewardBehaviour)
                .WithMany()
                .HasForeignKey(q => q.RewardBehaviourId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Calendar>()
                .HasOne(q => q.PenaltyBehaviour)
                .WithMany()
                .HasForeignKey(q => q.PenaltyBehaviourId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Calendar>()
                .HasOne(q => q.IgnoreBehaviour)
                .WithMany()
                .HasForeignKey(q => q.IgnoreBehaviourId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<cCalendarBehaviour>()
                .HasData(
                    new cCalendarBehaviour { Id = 1, NameEn = "Positive", NameRu = "Положительное", DescriptionEn = "On unmarked day, user will receive reward", DescriptionRu = "В день без отметки пользователь получит награду" },
                    new cCalendarBehaviour { Id = 2, NameEn = "Negative", NameRu = "Отрицательное", DescriptionEn = "On unmarked day, user will receive penalty", DescriptionRu = "В день без отметки пользователь получит штраф" },
                    new cCalendarBehaviour { Id = 3, NameEn = "Neutral", NameRu = "Нейтральное", DescriptionEn = "On unmarked day, status will remain unmarked", DescriptionRu = "В день без отметки статус останется неотмеченным" }
                );
            modelBuilder.Entity<cCalendarStatus>()
                .HasData(
                    new cCalendarStatus { Id = 1, NameEn = "Marked", NameRu = "Отмечено" },
                    new cCalendarStatus { Id = 2, NameEn = "Failed", NameRu = "Провалено" },
                    new cCalendarStatus { Id = 3, NameEn = "Not marked", NameRu = "Не отмечено" }
                );
            modelBuilder.Entity<cPopupInterval>()
                .HasData(
                    new cPopupInterval { Id = 1, NameEn = "Hourly", NameRu = "Почасовой" },
                    new cPopupInterval { Id = 2, NameEn = "Every half of day", NameRu = "Каждые полдня" },
                    new cPopupInterval { Id = 3, NameEn = "Daily", NameRu = "Ежедневно" },
                    new cPopupInterval { Id = 4, NameEn = "Weekly", NameRu = "Еженедельно" },
                    new cPopupInterval { Id = 5, NameEn = "Monthly", NameRu = "Ежемесячно" }
                );
            modelBuilder.Entity<HeroHabbit>()
                .HasOne(q => q.CheckinBehaviour)
                .WithMany()
                .HasForeignKey(q => q.CheckinBehaviourId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ExceptionLog>()
                .Property(x => x.ExceptionDateTime)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}