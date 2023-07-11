using Microsoft.EntityFrameworkCore;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> accounts { get; set; }

        public DbSet<Category> categories { get; set; }

        public DbSet<Favorite> favorites { get; set; }

        public DbSet<Rule> rules { get; set; }

        public DbSet<Scramble> scrambles { get; set; }

        public DbSet<Solve> solves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Account - Rule (one - many)
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Rule)
                .WithMany(r => r.Accounts)
                .HasForeignKey(a => a.RuleId);

            // Account - Favorite (many - one)
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Favorites)
                .WithOne(f => f.Account)
                .HasForeignKey(f => f.AccountId);

            // Account - Scramble (many - one)
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Scrambles)
                .WithOne(s => s.Account)
                .HasForeignKey(s => s.AccountId);

            // Favorite - Scramble (one - many)
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Scramble)
                .WithMany(s => s.Favorites)
                .HasForeignKey(f => f.ScrambleId);

            // Scramble - Category (one - many)
            modelBuilder.Entity<Scramble>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Scrambles)
                .HasForeignKey(s => s.CategoryId);

            // Scramble - Solve (one - one)
            modelBuilder.Entity<Scramble>()
                .HasOne(s => s.Solve)
                .WithOne(so => so.Scramble)
                .HasForeignKey<Solve>(so => so.ScrambleId);
        }
    }
}
