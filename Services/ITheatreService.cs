using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public interface ITheatreService
    {
        Task<Theatre> AddTheatreAsync(Theatre theatre);

        Task<List<Theatre>> GetAllTheatresAsync();

        Task<Theatre?> GetTheatreByIdAsync(int id);

        Task UpdateTheatreAsync(int id, Theatre theatre);

        Task DeleteTheatreAsync(int id);
    }
}