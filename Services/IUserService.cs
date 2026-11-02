
using MovieTicketBooking.DTOs;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterDto dto);

        Task<string> LoginAsync(LoginDto dto);
    }
}
