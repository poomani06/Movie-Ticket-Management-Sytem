using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly ApplicationDbContext _context;

        public ShowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Show> AddShowAsync(Show show)
        {
            await _context.Shows.AddAsync(show);
            await _context.SaveChangesAsync();

            return show;
        }

        public async Task<List<Show>> GetAllShowsAsync()
        {
            return await _context.Shows.ToListAsync();
        }

        public async Task<Show?> GetShowByIdAsync(int id)
        {
            return await _context.Shows
                .FirstOrDefaultAsync(s => s.ShowId == id);
        }

        public async Task<List<Show>> GetShowsByMovieIdAsync(int movieId)
        {
            return await _context.Shows
                .Where(s => s.MovieId == movieId)
                .ToListAsync();
        }

        public async Task<List<Show>> GetShowsByScreenIdAsync(int screenId)
        {
            return await _context.Shows
                .Where(s => s.ScreenId == screenId)
                .ToListAsync();
        }

        public async Task UpdateShowAsync(Show show)
        {
            _context.Shows.Update(show);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShowAsync(Show show)
        {
            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();
        }
        public async Task<List<ShowSeat>> GetShowSeatsByShowIdAsync(int showId)
        {
            return await _context.ShowSeats
                .Where(s => s.ShowId == showId)
                .ToListAsync();
        }

        public async Task<List<ShowSeat>> GetAvailableSeatsAsync(int showId)
        {
            return await _context.ShowSeats
                .Where(s => s.ShowId == showId && s.Status == "Available")
                .ToListAsync();
        }

        public async Task<List<ShowSeat>> GetBookedSeatsAsync(int showId)
        {
            return await _context.ShowSeats
                .Where(s => s.ShowId == showId && s.Status == "Booked")
                .ToListAsync();
        }

        public async Task AddShowSeatsAsync(List<ShowSeat> showSeats)
        {
            await _context.ShowSeats.AddRangeAsync(showSeats);
            await _context.SaveChangesAsync();
        }
        public async Task<ShowSeat?> GetShowSeatByShowAndSeatAsync(
    int showId, int seatId)
        {
            return await _context.ShowSeats
                .FirstOrDefaultAsync(s =>
                    s.ShowId == showId && s.SeatId == seatId);
        }

        public async Task UpdateShowSeatAsync(ShowSeat showSeat)
        {
            _context.ShowSeats.Update(showSeat);

            await _context.SaveChangesAsync();
        }
    }
}