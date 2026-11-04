
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MovieTicketBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IAuthorizationService _authorizationService;
        private int GetLoggedInUserId()
        {
            return int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
        public BookingsController(
    IBookingService bookingService,
    IAuthorizationService authorizationService)
        {
            _bookingService = bookingService;
            _authorizationService = authorizationService;
        }
        // POST: api/bookings
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateBooking(CreateBookingDto dto)
        {
            try
            {
                var userId = int.Parse(
                    User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var booking = await _bookingService.CreateBookingAsync(
                    userId,
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
        [Authorize(Roles = "Admin")]
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
        // GET: api/bookings/my-bookings
        [HttpGet("my-bookings")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> GetMyBookings()
        {
            var userId = GetLoggedInUserId();

            var bookings = await _bookingService.GetAllBookingsAsync();

            var response = bookings
                .Where(b => User.IsInRole("Admin") || b.UserId == userId)
                .Select(b => new BookingResponseDto
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
            var result = await _authorizationService.AuthorizeAsync(
    User,
    booking,
    "BookingOwner");

            if (!result.Succeeded)
            {
                return Forbid();
            }

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
            var result = await _authorizationService.AuthorizeAsync(
    User,
    booking,
    "BookingOwner");

            if (!result.Succeeded)
            {
                return Forbid();
            }
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
        // PUT: api/bookings/1/cancel
        [HttpPut("{id}/cancel")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            try
            {
                // Step 1: Find the booking
                var booking = await _bookingService.GetBookingByIdAsync(id);

                // Step 2: Check whether the booking exists
                if (booking == null)
                {
                    return NotFound("Booking not found.");
                }

                // Step 3: Check booking ownership
                // Step 3: Check booking ownership using custom authorization
                var result = await _authorizationService.AuthorizeAsync(
                    User,
                    booking,
                    "BookingOwner");

                if (!result.Succeeded)
                {
                    return Forbid();
                }

                // Step 4: Cancel the booking
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
