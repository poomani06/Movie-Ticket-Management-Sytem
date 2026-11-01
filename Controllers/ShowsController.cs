using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;

namespace MovieTicketBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowService _showService;

        public ShowsController(IShowService showService)
        {
            _showService = showService;
        }

        // POST: api/shows
        [HttpPost]
        public async Task<IActionResult> AddShow(CreateShowDto dto)
        {
            try
            {
                var show = new Show
                {
                    MovieId = dto.MovieId,
                    ScreenId = dto.ScreenId,
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                    TicketPrice = dto.TicketPrice
                };

                var result = await _showService.AddShowAsync(show);

                var response = new ShowResponseDto
                {
                    ShowId = result.ShowId,
                    MovieId = result.MovieId,
                    ScreenId = result.ScreenId,
                    StartTime = result.StartTime,
                    EndTime = result.EndTime,
                    TicketPrice = result.TicketPrice
                };

                return CreatedAtAction(
                    nameof(GetShowById),
                    new { id = response.ShowId },
                    response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/shows
        [HttpGet]
        public async Task<IActionResult> GetAllShows()
        {
            var shows = await _showService.GetAllShowsAsync();

            var response = shows.Select(show => new ShowResponseDto
            {
                ShowId = show.ShowId,
                MovieId = show.MovieId,
                ScreenId = show.ScreenId,
                StartTime = show.StartTime,
                EndTime = show.EndTime,
                TicketPrice = show.TicketPrice
            }).ToList();

            return Ok(response);
        }

        // GET: api/shows/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShowById(int id)
        {
            var show = await _showService.GetShowByIdAsync(id);

            if (show == null)
            {
                return NotFound("Show not found.");
            }

            var response = new ShowResponseDto
            {
                ShowId = show.ShowId,
                MovieId = show.MovieId,
                ScreenId = show.ScreenId,
                StartTime = show.StartTime,
                EndTime = show.EndTime,
                TicketPrice = show.TicketPrice
            };

            return Ok(response);
        }

        // GET: api/shows/movie/1
        [HttpGet("movie/{movieId}")]
        public async Task<IActionResult> GetShowsByMovieId(int movieId)
        {
            try
            {
                var shows = await _showService.GetShowsByMovieIdAsync(movieId);

                var response = shows.Select(show => new ShowResponseDto
                {
                    ShowId = show.ShowId,
                    MovieId = show.MovieId,
                    ScreenId = show.ScreenId,
                    StartTime = show.StartTime,
                    EndTime = show.EndTime,
                    TicketPrice = show.TicketPrice
                }).ToList();

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/shows/screen/1
        [HttpGet("screen/{screenId}")]
        public async Task<IActionResult> GetShowsByScreenId(int screenId)
        {
            try
            {
                var shows = await _showService.GetShowsByScreenIdAsync(screenId);

                var response = shows.Select(show => new ShowResponseDto
                {
                    ShowId = show.ShowId,
                    MovieId = show.MovieId,
                    ScreenId = show.ScreenId,
                    StartTime = show.StartTime,
                    EndTime = show.EndTime,
                    TicketPrice = show.TicketPrice
                }).ToList();

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/shows/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShow(int id, CreateShowDto dto)
        {
            try
            {
                var show = new Show
                {
                    MovieId = dto.MovieId,
                    ScreenId = dto.ScreenId,
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                    TicketPrice = dto.TicketPrice
                };

                await _showService.UpdateShowAsync(id, show);

                return Ok("Show updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/shows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            try
            {
                await _showService.DeleteShowAsync(id);

                return Ok("Show deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        // GET: api/shows/{showId}/seats
        [HttpGet("{showId}/seats")]
        public async Task<IActionResult> GetShowSeats(int showId)
        {
            try
            {
                var seats = await _showService.GetShowSeatsByShowIdAsync(showId);

                var response = seats.Select(seat => new ShowSeatResponseDto
                {
                    ShowSeatId = seat.ShowSeatId,
                    ShowId = seat.ShowId,
                    SeatId = seat.SeatId,
                    Status = seat.Status
                });

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/shows/{showId}/seats/available
        [HttpGet("{showId}/seats/available")]
        public async Task<IActionResult> GetAvailableSeats(int showId)
        {
            try
            {
                var seats = await _showService.GetAvailableSeatsAsync(showId);

                var response = seats.Select(seat => new ShowSeatResponseDto
                {
                    ShowSeatId = seat.ShowSeatId,
                    ShowId = seat.ShowId,
                    SeatId = seat.SeatId,
                    Status = seat.Status
                });

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/shows/{showId}/seats/booked
        [HttpGet("{showId}/seats/booked")]
        public async Task<IActionResult> GetBookedSeats(int showId)
        {
            try
            {
                var seats = await _showService.GetBookedSeatsAsync(showId);

                var response = seats.Select(seat => new ShowSeatResponseDto
                {
                    ShowSeatId = seat.ShowSeatId,
                    ShowId = seat.ShowId,
                    SeatId = seat.SeatId,
                    Status = seat.Status
                });

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}