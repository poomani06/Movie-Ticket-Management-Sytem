
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);

            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
        }

        public async Task<Movie?> SearchMovieAsync(string title)
        {
            return await _context.Movies
                .FirstOrDefaultAsync(m => m.Title == title);
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Movies.Update(movie);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(Movie movie)
        {
            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();
        }
    }
}
