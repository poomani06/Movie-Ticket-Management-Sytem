using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Microsoft.AspNetCore.Authorization;

namespace MovieTicketBooking.Controllers
{
    [Route("api/theatres")]
    [ApiController]
    [Authorize]
    public class TheatresController : ControllerBase
    {
        private readonly ITheatreService _theatreService;

        public TheatresController(ITheatreService theatreService)
        {
            _theatreService = theatreService;
        }

        // POST: Add Theatre
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTheatre(CreateTheatreDto dto)
        {
            try
            {
                Theatre theatre = new Theatre
                {
                    Name = dto.Name,
                    Location = dto.Location
                };

                var result = await _theatreService.AddTheatreAsync(theatre);

                TheatreResponseDto response = new TheatreResponseDto
                {
                    TheatreId = result.TheatreId,
                    Name = result.Name,
                    Location = result.Location
                };

                return CreatedAtAction(
                    nameof(GetTheatreById),
                    new { id = result.TheatreId },
                    response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: Get All Theatres
        [HttpGet]
        public async Task<IActionResult> GetAllTheatres()
        {
            var theatres = await _theatreService.GetAllTheatresAsync();

            var response = theatres.Select(t => new TheatreResponseDto
            {
                TheatreId = t.TheatreId,
                Name = t.Name,
                Location = t.Location
            }).ToList();

            return Ok(response);
        }

        // GET: Get Theatre By ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTheatreById(int id)
        {
            var theatre = await _theatreService.GetTheatreByIdAsync(id);

            if (theatre == null)
            {
                return NotFound(new { message = "Theatre not found." });
            }

            TheatreResponseDto response = new TheatreResponseDto
            {
                TheatreId = theatre.TheatreId,
                Name = theatre.Name,
                Location = theatre.Location
            };

            return Ok(response);
        }

        // PUT: Update Theatre
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTheatre(
            int id, CreateTheatreDto dto)
        {
            try
            {
                Theatre theatre = new Theatre
                {
                    Name = dto.Name,
                    Location = dto.Location
                };

                await _theatreService.UpdateTheatreAsync(id, theatre);

                return Ok(new { message = "Theatre updated successfully." });
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

        // DELETE: Delete Theatre
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTheatre(int id)
        {
            try
            {
                await _theatreService.DeleteTheatreAsync(id);

                return Ok(new { message = "Theatre deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}