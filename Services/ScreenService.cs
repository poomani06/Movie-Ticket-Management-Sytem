using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories;

namespace MovieTicketBooking.Services
{
    public class ScreenService : IScreenService
    {
        private readonly IScreenRepository _screenRepository;
        private readonly ITheatreRepository _theatreRepository;

        public ScreenService(
            IScreenRepository screenRepository,
            ITheatreRepository theatreRepository)
        {
            _screenRepository = screenRepository;
            _theatreRepository = theatreRepository;
        }

        public async Task<Screen> AddScreenAsync(Screen screen)
        {
            if (string.IsNullOrWhiteSpace(screen.ScreenName))
            {
                throw new ArgumentException("Screen name is required.");
            }

            var theatre = await _theatreRepository.GetTheatreByIdAsync(screen.TheatreId);

            if (theatre == null)
            {
                throw new KeyNotFoundException("Theatre not found.");
            }

            return await _screenRepository.AddScreenAsync(screen);
        }

        public async Task<List<Screen>> GetAllScreensAsync()
        {
            return await _screenRepository.GetAllScreensAsync();
        }

        public async Task<Screen?> GetScreenByIdAsync(int id)
        {
            return await _screenRepository.GetScreenByIdAsync(id);
        }

        public async Task<List<Screen>> GetScreensByTheatreIdAsync(int theatreId)
        {
            var theatre = await _theatreRepository.GetTheatreByIdAsync(theatreId);

            if (theatre == null)
            {
                throw new KeyNotFoundException("Theatre not found.");
            }

            return await _screenRepository.GetScreensByTheatreIdAsync(theatreId);
        }

        public async Task UpdateScreenAsync(int id, Screen screen)
        {
            if (string.IsNullOrWhiteSpace(screen.ScreenName))
            {
                throw new ArgumentException("Screen name is required.");
            }

            var existingScreen = await _screenRepository.GetScreenByIdAsync(id);

            if (existingScreen == null)
            {
                throw new KeyNotFoundException("Screen not found.");
            }

            var theatre = await _theatreRepository.GetTheatreByIdAsync(screen.TheatreId);

            if (theatre == null)
            {
                throw new KeyNotFoundException("Theatre not found.");
            }

            existingScreen.ScreenName = screen.ScreenName;
            existingScreen.TheatreId = screen.TheatreId;

            await _screenRepository.UpdateScreenAsync(existingScreen);
        }

        public async Task DeleteScreenAsync(int id)
        {
            var screen = await _screenRepository.GetScreenByIdAsync(id);

            if (screen == null)
            {
                throw new KeyNotFoundException("Screen not found.");
            }

            await _screenRepository.DeleteScreenAsync(screen);
        }
    }
}