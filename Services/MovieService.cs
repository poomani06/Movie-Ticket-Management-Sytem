
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories;

namespace MovieTicketBooking.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            if (string.IsNullOrWhiteSpace(movie.Title))
            {
                throw new ArgumentException("Movie title is required.");
            }

            if (movie.DurationMinutes <= 0)
            {
                throw new ArgumentException("Movie duration must be greater than zero.");
            }

            return await _movieRepository.AddMovieAsync(movie);
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllMoviesAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await _movieRepository.GetMovieByIdAsync(id);
        }

        public async Task<Movie?> SearchMovieAsync(string title)
        {
            return await _movieRepository.SearchMovieAsync(title);
        }

        public async Task UpdateMovieAsync(int id, Movie movie)
        {
            var existingMovie = await _movieRepository.GetMovieByIdAsync(id);

            if (existingMovie == null)
            {
                throw new KeyNotFoundException("Movie not found.");
            }

            if (string.IsNullOrWhiteSpace(movie.Title))
            {
                throw new ArgumentException("Movie title is required.");
            }

            if (movie.DurationMinutes <= 0)
            {
                throw new ArgumentException("Movie duration must be greater than zero.");
            }

            existingMovie.Title = movie.Title;
            existingMovie.Description = movie.Description;
            existingMovie.DurationMinutes = movie.DurationMinutes;
            existingMovie.ReleaseDate = movie.ReleaseDate;
            existingMovie.Language = movie.Language;
            existingMovie.Genre = movie.Genre;

            await _movieRepository.UpdateMovieAsync(existingMovie);
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);

            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found.");
            }

            await _movieRepository.DeleteMovieAsync(movie);
        }
    }
}
