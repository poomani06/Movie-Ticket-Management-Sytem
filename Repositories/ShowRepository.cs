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
    }
}