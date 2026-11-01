using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public class TheatreRepository : ITheatreRepository
    {
        private readonly ApplicationDbContext _context;

        public TheatreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add Theatre
        public async Task<Theatre> AddTheatreAsync(Theatre theatre)
        {
            await _context.Theatres.AddAsync(theatre);
            await _context.SaveChangesAsync();

            return theatre;
        }

        // Get All Theatres
        public async Task<List<Theatre>> GetAllTheatresAsync()
        {
            return await _context.Theatres.ToListAsync();
        }

        // Get Theatre By ID
        public async Task<Theatre?> GetTheatreByIdAsync(int id)
        {
            return await _context.Theatres
                .FirstOrDefaultAsync(t => t.TheatreId == id);
        }

        // Update Theatre
        public async Task UpdateTheatreAsync(Theatre theatre)
        {
            _context.Theatres.Update(theatre);
            await _context.SaveChangesAsync();
        }

        // Delete Theatre
        public async Task DeleteTheatreAsync(Theatre theatre)
        {
            _context.Theatres.Remove(theatre);
            await _context.SaveChangesAsync();
        }
    }
}