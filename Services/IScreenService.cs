using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public interface IScreenService
    {
        Task<Screen> AddScreenAsync(Screen screen);

        Task<List<Screen>> GetAllScreensAsync();

        Task<Screen?> GetScreenByIdAsync(int id);

        Task<List<Screen>> GetScreensByTheatreIdAsync(int theatreId);

        Task UpdateScreenAsync(int id, Screen screen);

        Task DeleteScreenAsync(int id);
    }
}