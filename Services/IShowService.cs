using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public interface IShowService
    {
        Task<Show> AddShowAsync(Show show);

        Task<List<Show>> GetAllShowsAsync();

        Task<Show?> GetShowByIdAsync(int id);

        Task<List<Show>> GetShowsByMovieIdAsync(int movieId);

        Task<List<Show>> GetShowsByScreenIdAsync(int screenId);

        Task UpdateShowAsync(int id, Show show);

        Task DeleteShowAsync(int id);
    }
}