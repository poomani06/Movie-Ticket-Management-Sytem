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
        public DbSet<Show> Shows { get; set; }
        public DbSet<ShowSeat> ShowSeats { get; set; }
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
            modelBuilder.Entity<Show>()
    .HasOne<Movie>()
    .WithMany()
    .HasForeignKey(s => s.MovieId);
            modelBuilder.Entity<Show>()
    .HasOne<Screen>()
    .WithMany()
    .HasForeignKey(s => s.ScreenId);
            modelBuilder.Entity<ShowSeat>()
    .HasOne<Show>()
    .WithMany()
    .HasForeignKey(ss => ss.ShowId)
    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ShowSeat>()
                .HasOne<Seat>()
                .WithMany()
                .HasForeignKey(ss => ss.SeatId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Show>()
    .Property(s => s.TicketPrice)
    .HasPrecision(10, 2);

        }
    }
    }


