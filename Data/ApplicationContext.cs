using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Actor> Actors { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Row> Rows { get; set; }

        public DbSet<ScenarioWriter> ScenarioWriters { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<SessionSeat> SessionSeats { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<SessionSeat>()
            .HasKey(s => new { s.SessionId, s.SeatId });
            builder.Entity<Review>()
            .HasIndex(s => new { s.MovieId, s.UserId }).IsUnique();
            builder.Entity<Country>()
            .HasIndex(s => s.Name).IsUnique();
        }
    }
}
