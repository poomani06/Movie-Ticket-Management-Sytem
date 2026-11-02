using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Microsoft.AspNetCore.Authorization;

namespace MovieTicketBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ScreensController : ControllerBase
    {
        private readonly IScreenService _screenService;

        public ScreensController(IScreenService screenService)
        {
            _screenService = screenService;
        }

        // POST: api/screens
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddScreen(CreateScreenDto dto)
        {
            try
            {
                var screen = new Screen
                {
                    TheatreId = dto.TheatreId,
                    ScreenName = dto.ScreenName
                };

                var result = await _screenService.AddScreenAsync(screen);

                var response = new ScreenResponseDto
                {
                    ScreenId = result.ScreenId,
                    TheatreId = result.TheatreId,
                    ScreenName = result.ScreenName
                };

                return CreatedAtAction(nameof(GetScreenById),
                    new { id = response.ScreenId }, response);
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

        // GET: api/screens
        [HttpGet]

        public async Task<IActionResult> GetAllScreens()
        {
            var screens = await _screenService.GetAllScreensAsync();

            var response = screens.Select(s => new ScreenResponseDto
            {
                ScreenId = s.ScreenId,
                TheatreId = s.TheatreId,
                ScreenName = s.ScreenName
            }).ToList();

            return Ok(response);
        }

        // GET: api/screens/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScreenById(int id)
        {
            var screen = await _screenService.GetScreenByIdAsync(id);

            if (screen == null)
            {
                return NotFound("Screen not found.");
            }

            var response = new ScreenResponseDto
            {
                ScreenId = screen.ScreenId,
                TheatreId = screen.TheatreId,
                ScreenName = screen.ScreenName
            };

            return Ok(response);
        }

        // GET: api/screens/theatre/1
        [HttpGet("theatre/{theatreId}")]
        public async Task<IActionResult> GetScreensByTheatreId(int theatreId)
        {
            try
            {
                var screens = await _screenService.GetScreensByTheatreIdAsync(theatreId);

                var response = screens.Select(s => new ScreenResponseDto
                {
                    ScreenId = s.ScreenId,
                    TheatreId = s.TheatreId,
                    ScreenName = s.ScreenName
                }).ToList();

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/screens/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateScreen(int id, CreateScreenDto dto)
        {
            try
            {
                var screen = new Screen
                {
                    TheatreId = dto.TheatreId,
                    ScreenName = dto.ScreenName
                };

                await _screenService.UpdateScreenAsync(id, screen);

                return Ok("Screen updated successfully.");
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

        // DELETE: api/screens/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteScreen(int id)
        {
            try
            {
                await _screenService.DeleteScreenAsync(id);

                return Ok("Screen deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}