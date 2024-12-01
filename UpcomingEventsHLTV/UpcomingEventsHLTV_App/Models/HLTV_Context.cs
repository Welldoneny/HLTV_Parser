using Microsoft.EntityFrameworkCore;

namespace UpcomingEventsHLTV_App.Models
{
    class HLTV_Context : DbContext
    {
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Tournament_Team> Tournament_Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HLTV_DB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tournament_Team>()
                .HasKey(tt => new { tt.TeamId, tt.TournamentId });

            modelBuilder.Entity<Tournament_Team>()
                .HasOne(tt => tt.Team)
                .WithMany(t => t.Tournament_Teams)
                .HasForeignKey(tt => tt.TeamId);

            modelBuilder.Entity<Tournament_Team>()
                .HasOne(tt => tt.tournament)
                .WithMany(t => t.Tournament_Teams)
                .HasForeignKey(tt => tt.TournamentId);
        }
        public HLTV_Context() => Database.EnsureCreated();
    }
}
