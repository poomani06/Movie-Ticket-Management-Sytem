
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Data
{
    public class SeedData
    {

        public static async Task InitializeAsync(
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            var adminPassword = configuration["SeedUsers:AdminPassword"];

            if (string.IsNullOrWhiteSpace(adminPassword))
            {
                throw new InvalidOperationException(
                    "Admin password is not configured in User Secrets.");
            }

            var admin = await context.Users
                .FirstOrDefaultAsync(u => u.Email == "admin@gmail.com");

            if (admin == null)
            {
                admin = new User
                {
                    Name = "Movie Admin",
                    Email = "admin@gmail.com",
                    Phone = "9999999999",
                    Role = "Admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminPassword)
                };

                await context.Users.AddAsync(admin);
            }
            else if (!BCrypt.Net.BCrypt.Verify(adminPassword, admin.PasswordHash))
            {
                admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminPassword);
            }

            await context.SaveChangesAsync();
        }


    }
}
