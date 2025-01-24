using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace I_am_Hero_API.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Token>()
                .Property(b => b.CreateDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}
