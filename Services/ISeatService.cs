using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public interface ISeatService
    {
        Task<Seat> AddSeatAsync(Seat seat);

        Task<List<Seat>> AddSeatsAsync(List<Seat> seats);

        Task<List<Seat>> GetSeatsByScreenIdAsync(int screenId);

        Task<Seat?> GetSeatByIdAsync(int id);

        Task UpdateSeatAsync(int id, Seat seat);

        Task DeleteSeatAsync(int id);
    }
}
