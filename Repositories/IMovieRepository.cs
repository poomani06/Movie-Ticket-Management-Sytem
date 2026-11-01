
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> AddMovieAsync(Movie movie);

        Task<List<Movie>> GetAllMoviesAsync();

        Task<Movie?> GetMovieByIdAsync(int id);

        Task<Movie?> SearchMovieAsync(string title);

        Task UpdateMovieAsync(Movie movie);

        Task DeleteMovieAsync(Movie movie);
    }
}
