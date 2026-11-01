
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;

namespace MovieTicketBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<IActionResult> CreateBooking(
            CreateBookingDto dto)
        {
            try
            {
                var booking = await _bookingService.CreateBookingAsync(
                    dto.CustomerName,
                    dto.CustomerEmail,
                    dto.CustomerPhone,
                    dto.ShowId,
                    dto.SeatIds);

                var response = new BookingResponseDto
                {
                    BookingId = booking.BookingId,
                    BookingReference = booking.BookingReference,
                    ShowId = booking.ShowId,
                    CustomerName = booking.CustomerName,
                    CustomerEmail = booking.CustomerEmail,
                    CustomerPhone = booking.CustomerPhone,
                    BookingDate = booking.BookingDate,
                    TotalAmount = booking.TotalAmount,
                    Status = booking.Status
                };

                return CreatedAtAction(
                    nameof(GetBookingById),
                    new { id = booking.BookingId },
                    response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/bookings
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();

            var response = bookings.Select(b => new BookingResponseDto
            {
                BookingId = b.BookingId,
                BookingReference = b.BookingReference,
                ShowId = b.ShowId,
                CustomerName = b.CustomerName,
                CustomerEmail = b.CustomerEmail,
                CustomerPhone = b.CustomerPhone,
                BookingDate = b.BookingDate,
                TotalAmount = b.TotalAmount,
                Status = b.Status
            });

            return Ok(response);
        }

        // GET: api/bookings/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);

            if (booking == null)
                return NotFound("Booking not found.");

            var response = new BookingResponseDto
            {
                BookingId = booking.BookingId,
                BookingReference = booking.BookingReference,
                ShowId = booking.ShowId,
                CustomerName = booking.CustomerName,
                CustomerEmail = booking.CustomerEmail,
                CustomerPhone = booking.CustomerPhone,
                BookingDate = booking.BookingDate,
                TotalAmount = booking.TotalAmount,
                Status = booking.Status
            };

            return Ok(response);
        }

        // GET: api/bookings/reference/BK12345678
        [HttpGet("reference/{reference}")]
        public async Task<IActionResult> GetBookingByReference(
            string reference)
        {
            var booking = await _bookingService
                .GetBookingByReferenceAsync(reference);

            if (booking == null)
                return NotFound("Booking not found.");

            var response = new BookingResponseDto
            {
                BookingId = booking.BookingId,
                BookingReference = booking.BookingReference,
                ShowId = booking.ShowId,
                CustomerName = booking.CustomerName,
                CustomerEmail = booking.CustomerEmail,
                CustomerPhone = booking.CustomerPhone,
                BookingDate = booking.BookingDate,
                TotalAmount = booking.TotalAmount,
                Status = booking.Status
            };

            return Ok(response);
        }

        // PUT: api/bookings/1/cancel
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            try
            {
                await _bookingService.CancelBookingAsync(id);

                return Ok("Booking cancelled successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
