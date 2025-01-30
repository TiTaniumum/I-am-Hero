using I_am_Hero_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace I_am_Hero_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Hero> Heroes {  get; set; }
        public DbSet<HeroSkill> HeroSkills { get; set; }
        public DbSet<cLevelCalculationType> cLevelCalculationTypes { get; set; }
        public DbSet<HeroAttribute> HeroAttributes { get; set; }
        public DbSet<cAttributeType> cAttributeTypes { get; set; }
        public DbSet<HeroAttributeState> HeroAttributeStates { get; set; }
        public DbSet<HeroStatusEffect> HeroStatusEffects { get; set; } 
        public DbSet<HeroBioPiece> HeroBioPieces { get; set; }
        public DbSet<HeroAchievement> HeroAchievements { get; set; }
        public DbSet<cRarity> cRarities { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.IsEmailVerified)
                .HasDefaultValue(false);
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
                
        }
    }
}
