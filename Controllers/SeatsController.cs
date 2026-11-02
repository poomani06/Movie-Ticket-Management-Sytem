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
    public class SeatsController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatsController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        // POST: api/seats
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSeat(CreateSeatDto dto)
        {
            try
            {
                var seat = new Seat
                {
                    ScreenId = dto.ScreenId,
                    RowName = dto.RowName,
                    SeatNumber = dto.SeatNumber,
                    SeatType = dto.SeatType
                };

                var result = await _seatService.AddSeatAsync(seat);

                var response = new SeatResponseDto
                {
                    SeatId = result.SeatId,
                    ScreenId = result.ScreenId,
                    RowName = result.RowName,
                    SeatNumber = result.SeatNumber,
                    SeatType = result.SeatType
                };

                return Ok(response);
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

        // POST: api/seats/bulk

        [HttpPost("bulk")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddSeats(List<CreateSeatDto> dtos)
        {
            try
            {
                var seats = dtos.Select(dto => new Seat
                {
                    ScreenId = dto.ScreenId,
                    RowName = dto.RowName,
                    SeatNumber = dto.SeatNumber,
                    SeatType = dto.SeatType
                }).ToList();

                var result = await _seatService.AddSeatsAsync(seats);

                var response = result.Select(s => new SeatResponseDto
                {
                    SeatId = s.SeatId,
                    ScreenId = s.ScreenId,
                    RowName = s.RowName,
                    SeatNumber = s.SeatNumber,
                    SeatType = s.SeatType
                }).ToList();

                return Ok(response);
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

        // GET: api/seats/screen/1
        [HttpGet("screen/{screenId}")]
        public async Task<IActionResult> GetSeatsByScreenId(int screenId)
        {
            try
            {
                var seats = await _seatService.GetSeatsByScreenIdAsync(screenId);

                var response = seats.Select(s => new SeatResponseDto
                {
                    SeatId = s.SeatId,
                    ScreenId = s.ScreenId,
                    RowName = s.RowName,
                    SeatNumber = s.SeatNumber,
                    SeatType = s.SeatType
                }).ToList();

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/seats/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeatById(int id)
        {
            var seat = await _seatService.GetSeatByIdAsync(id);

            if (seat == null)
            {
                return NotFound("Seat not found.");
            }

            var response = new SeatResponseDto
            {
                SeatId = seat.SeatId,
                ScreenId = seat.ScreenId,
                RowName = seat.RowName,
                SeatNumber = seat.SeatNumber,
                SeatType = seat.SeatType
            };

            return Ok(response);
        }

        // PUT: api/seats/1
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSeat(int id, CreateSeatDto dto)
        {
            try
            {
                var seat = new Seat
                {
                    ScreenId = dto.ScreenId,
                    RowName = dto.RowName,
                    SeatNumber = dto.SeatNumber,
                    SeatType = dto.SeatType
                };

                await _seatService.UpdateSeatAsync(id, seat);

                return Ok("Seat updated successfully.");
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

        // DELETE: api/seats/1
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            try
            {
                await _seatService.DeleteSeatAsync(id);

                return Ok("Seat deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}