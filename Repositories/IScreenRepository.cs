using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public interface IScreenRepository
    {
        Task<Screen> AddScreenAsync(Screen screen);

        Task<List<Screen>> GetAllScreensAsync();

        Task<Screen?> GetScreenByIdAsync(int id);

        Task<List<Screen>> GetScreensByTheatreIdAsync(int theatreId);

        Task UpdateScreenAsync(Screen screen);

        Task DeleteScreenAsync(Screen screen);
    }
}