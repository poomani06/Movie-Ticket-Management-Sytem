
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public interface IBookingService
    {
        Task<Booking> CreateBookingAsync(
            string customerName,
            string customerEmail,
            string customerPhone,
            int showId,
            List<int> seatIds);

        Task<List<Booking>> GetAllBookingsAsync();

        Task<Booking?> GetBookingByIdAsync(int id);

        Task<Booking?> GetBookingByReferenceAsync(string reference);

        Task CancelBookingAsync(int bookingId);
    }
}
