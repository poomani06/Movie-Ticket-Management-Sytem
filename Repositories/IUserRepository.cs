
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);

        Task<User> AddUserAsync(User user);
    }
}
