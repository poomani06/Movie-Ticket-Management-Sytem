using Microsoft.AspNetCore.Authorization;
using MovieTicketBooking.Authorization.Requirements;
using MovieTicketBooking.Models;
using System.Security.Claims;

namespace MovieTicketBooking.Authorization.Handlers
{
    public class BookingOwnerHandler
        : AuthorizationHandler<BookingOwnerRequirement, Booking>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            BookingOwnerRequirement requirement,
            Booking booking)
        {
            // Admin can access any booking
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            // Get logged-in user's ID from JWT
            var userId = context.User.FindFirst(
                ClaimTypes.NameIdentifier)?.Value;

            // Check booking ownership
            if (userId != null &&
                booking.UserId?.ToString() == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}