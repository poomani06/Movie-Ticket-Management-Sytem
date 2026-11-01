using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public interface IShowRepository
    {
        Task<Show> AddShowAsync(Show show);

        Task<List<Show>> GetAllShowsAsync();

        Task<Show?> GetShowByIdAsync(int id);

        Task<List<Show>> GetShowsByMovieIdAsync(int movieId);

        Task<List<Show>> GetShowsByScreenIdAsync(int screenId);

        Task UpdateShowAsync(Show show);

        Task DeleteShowAsync(Show show);
    }
}