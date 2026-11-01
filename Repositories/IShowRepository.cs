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
        Task<List<ShowSeat>> GetShowSeatsByShowIdAsync(int showId);

        Task<List<ShowSeat>> GetAvailableSeatsAsync(int showId);

        Task<List<ShowSeat>> GetBookedSeatsAsync(int showId);

        Task AddShowSeatsAsync(List<ShowSeat> showSeats);
    }
}