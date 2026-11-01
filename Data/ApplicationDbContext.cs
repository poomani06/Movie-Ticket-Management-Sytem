using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Data
{

        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<Movie> Movies { get; set; }
            public DbSet<Theatre> Theatres { get; set; }
            public DbSet<Screen> Screens { get; set; }
            public DbSet<Seat> Seats { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Screen>()
                .HasOne<Theatre>()
                .WithMany()
                .HasForeignKey(s => s.TheatreId);
            modelBuilder.Entity<Seat>()
                .HasOne<Screen>()
                .WithMany()
                .HasForeignKey(s => s.ScreenId);
            modelBuilder.Entity<Seat>()
                .HasIndex(s => new { s.ScreenId, s.RowName, s.SeatNumber })
                .IsUnique();
        }
    }
    }


