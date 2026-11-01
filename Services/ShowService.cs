using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories;

namespace MovieTicketBooking.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowRepository _showRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IScreenRepository _screenRepository;
        private readonly ISeatRepository _seatRepository;

        public ShowService(
    IShowRepository showRepository,
    IMovieRepository movieRepository,
    IScreenRepository screenRepository,
    ISeatRepository seatRepository)
        {
            _showRepository = showRepository;
            _movieRepository = movieRepository;
            _screenRepository = screenRepository;
            _seatRepository = seatRepository;
        }

        public async Task<Show> AddShowAsync(Show show)
        {
            await ValidateShowAsync(show);

            var existingShows = await _showRepository
                .GetShowsByScreenIdAsync(show.ScreenId);

            foreach (var existingShow in existingShows)
            {
                if (show.StartTime < existingShow.EndTime &&
                    show.EndTime > existingShow.StartTime)
                {
                    throw new ArgumentException(
                        "Another show is already scheduled during this time.");
                }
            }

            // Save the Show
            var createdShow = await _showRepository.AddShowAsync(show);

            // Get all physical seats for the selected screen
            var seats = await _seatRepository
                .GetSeatsByScreenIdAsync(show.ScreenId);

            // Create ShowSeat records with Available status
            var showSeats = seats.Select(seat => new ShowSeat
            {
                ShowId = createdShow.ShowId,
                SeatId = seat.SeatId,
                Status = "Available"
            }).ToList();

            // Save all ShowSeats
            if (showSeats.Any())
            {
                await _showRepository.AddShowSeatsAsync(showSeats);
            }

            return createdShow;
        }

        public async Task<List<Show>> GetAllShowsAsync()
        {
            return await _showRepository.GetAllShowsAsync();
        }

        public async Task<Show?> GetShowByIdAsync(int id)
        {
            return await _showRepository.GetShowByIdAsync(id);
        }

        public async Task<List<Show>> GetShowsByMovieIdAsync(int movieId)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(movieId);

            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found.");
            }

            return await _showRepository.GetShowsByMovieIdAsync(movieId);
        }

        public async Task<List<Show>> GetShowsByScreenIdAsync(int screenId)
        {
            var screen = await _screenRepository.GetScreenByIdAsync(screenId);

            if (screen == null)
            {
                throw new KeyNotFoundException("Screen not found.");
            }

            return await _showRepository.GetShowsByScreenIdAsync(screenId);
        }

        public async Task UpdateShowAsync(int id, Show show)
        {
            await ValidateShowAsync(show);

            var existingShow = await _showRepository.GetShowByIdAsync(id);

            if (existingShow == null)
            {
                throw new KeyNotFoundException("Show not found.");
            }

            var existingShows = await _showRepository
                .GetShowsByScreenIdAsync(show.ScreenId);

            foreach (var otherShow in existingShows)
            {
                if (otherShow.ShowId != id &&
                    show.StartTime < otherShow.EndTime &&
                    show.EndTime > otherShow.StartTime)
                {
                    throw new ArgumentException(
                        "Another show is already scheduled during this time.");
                }
            }

            existingShow.MovieId = show.MovieId;
            existingShow.ScreenId = show.ScreenId;
            existingShow.StartTime = show.StartTime;
            existingShow.EndTime = show.EndTime;
            existingShow.TicketPrice = show.TicketPrice;

            await _showRepository.UpdateShowAsync(existingShow);
        }

        public async Task DeleteShowAsync(int id)
        {
            var show = await _showRepository.GetShowByIdAsync(id);

            if (show == null)
            {
                throw new KeyNotFoundException("Show not found.");
            }

            await _showRepository.DeleteShowAsync(show);
        }

        private async Task ValidateShowAsync(Show show)
        {
            if (show.EndTime <= show.StartTime)
            {
                throw new ArgumentException(
                    "End time must be later than start time.");
            }

            if (show.TicketPrice < 0)
            {
                throw new ArgumentException(
                    "Ticket price cannot be negative.");
            }

            var movie = await _movieRepository.GetMovieByIdAsync(show.MovieId);

            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found.");
            }

            var screen = await _screenRepository.GetScreenByIdAsync(show.ScreenId);

            if (screen == null)
            {
                throw new KeyNotFoundException("Screen not found.");
            }
        }
        public async Task<List<ShowSeat>> GetShowSeatsByShowIdAsync(int showId)
        {
            var show = await _showRepository.GetShowByIdAsync(showId);

            if (show == null)
            {
                throw new KeyNotFoundException("Show not found.");
            }

            return await _showRepository.GetShowSeatsByShowIdAsync(showId);
        }

        public async Task<List<ShowSeat>> GetAvailableSeatsAsync(int showId)
        {
            var show = await _showRepository.GetShowByIdAsync(showId);

            if (show == null)
            {
                throw new KeyNotFoundException("Show not found.");
            }

            return await _showRepository.GetAvailableSeatsAsync(showId);
        }

        public async Task<List<ShowSeat>> GetBookedSeatsAsync(int showId)
        {
            var show = await _showRepository.GetShowByIdAsync(showId);

            if (show == null)
            {
                throw new KeyNotFoundException("Show not found.");
            }

            return await _showRepository.GetBookedSeatsAsync(showId);
        }
    }
}