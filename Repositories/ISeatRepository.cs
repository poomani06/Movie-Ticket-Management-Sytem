using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public interface ISeatRepository
    {
        Task<Seat> AddSeatAsync(Seat seat);

        Task<List<Seat>> AddSeatsAsync(List<Seat> seats);

        Task<List<Seat>> GetSeatsByScreenIdAsync(int screenId);

        Task<Seat?> GetSeatByIdAsync(int id);

        Task UpdateSeatAsync(Seat seat);

        Task DeleteSeatAsync(Seat seat);
    }
}