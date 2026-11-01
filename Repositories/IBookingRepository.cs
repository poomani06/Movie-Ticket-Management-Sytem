
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> AddBookingAsync(Booking booking);

        Task<List<Booking>> GetAllBookingsAsync();

        Task<Booking?> GetBookingByIdAsync(int id);

        Task<Booking?> GetBookingByReferenceAsync(string reference);

        Task<List<BookingSeat>> GetBookingSeatsAsync(int bookingId);

        Task AddBookingSeatsAsync(List<BookingSeat> bookingSeats);

        Task UpdateBookingAsync(Booking booking);
        Task<Booking> CreateBookingWithSeatsAsync(
    Booking booking,
    List<BookingSeat> bookingSeats,
    List<ShowSeat> showSeats);
        Task CancelBookingWithSeatsAsync(
    Booking booking,
    List<ShowSeat> showSeats);
    }
}
