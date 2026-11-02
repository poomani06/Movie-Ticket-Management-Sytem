
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Microsoft.AspNetCore.Authorization;

namespace MovieTicketBooking.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // POST: api/movies
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovie([FromBody] CreateMovieDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                DurationMinutes = dto.DurationMinutes,
                ReleaseDate = dto.ReleaseDate,
                Language = dto.Language,
                Genre = dto.Genre
            };

            try
            {
                var result = await _movieService.AddMovieAsync(movie);

                var response = new MovieResponseDto
                {
                    MovieId = result.MovieId,
                    Title = result.Title,
                    Description = result.Description,
                    DurationMinutes = result.DurationMinutes,
                    ReleaseDate = result.ReleaseDate,
                    Language = result.Language,
                    Genre = result.Genre
                };

                return CreatedAtAction(
                    nameof(GetMovieById),
                    new { id = result.MovieId },
                    response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/movies
        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();

            var response = movies.Select(movie => new MovieResponseDto
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                DurationMinutes = movie.DurationMinutes,
                ReleaseDate = movie.ReleaseDate,
                Language = movie.Language,
                Genre = movie.Genre
            }).ToList();

            return Ok(response);
        }

        // GET: api/movies/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound(new { message = "Movie not found." });
            }

            var response = new MovieResponseDto
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                DurationMinutes = movie.DurationMinutes,
                ReleaseDate = movie.ReleaseDate,
                Language = movie.Language,
                Genre = movie.Genre
            };

            return Ok(response);
        }

        // GET: api/movies/search?title=Leo
        [HttpGet("search")]
        public async Task<IActionResult> SearchMovie(string title)
        {
            var movie = await _movieService.SearchMovieAsync(title);

            if (movie == null)
            {
                return NotFound(new { message = "Movie not found." });
            }

            var response = new MovieResponseDto
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Description = movie.Description,
                DurationMinutes = movie.DurationMinutes,
                ReleaseDate = movie.ReleaseDate,
                Language = movie.Language,
                Genre = movie.Genre
            };

            return Ok(response);
        }

        // PUT: api/movies/1
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMovie(int id, CreateMovieDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                DurationMinutes = dto.DurationMinutes,
                ReleaseDate = dto.ReleaseDate,
                Language = dto.Language,
                Genre = dto.Genre
            };

            try
            {
                await _movieService.UpdateMovieAsync(id, movie);
                return Ok(new { message = "Movie updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE: api/movies/1
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            try
            {
                await _movieService.DeleteMovieAsync(id);
                return Ok(new { message = "Movie deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
