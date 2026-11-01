
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public interface IMovieService
    {
        Task<Movie> AddMovieAsync(Movie movie);

        Task<List<Movie>> GetAllMoviesAsync();

        Task<Movie?> GetMovieByIdAsync(int id);

        Task<Movie?> SearchMovieAsync(string title);

        Task UpdateMovieAsync(int id, Movie movie);

        Task DeleteMovieAsync(int id);
    }
}
