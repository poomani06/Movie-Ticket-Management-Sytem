using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public interface ITheatreRepository
    {
        Task<Theatre> AddTheatreAsync(Theatre theatre);

        Task<List<Theatre>> GetAllTheatresAsync();

        Task<Theatre?> GetTheatreByIdAsync(int id);

        Task UpdateTheatreAsync(Theatre theatre);

        Task DeleteTheatreAsync(Theatre theatre);
    }
}