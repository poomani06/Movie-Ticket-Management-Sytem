
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<Booking?> GetBookingByReferenceAsync(string reference)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(b => b.BookingReference == reference);
        }

        public async Task<List<BookingSeat>> GetBookingSeatsAsync(int bookingId)
        {
            return await _context.BookingSeats
                .Where(bs => bs.BookingId == bookingId)
                .ToListAsync();
        }

        public async Task AddBookingSeatsAsync(List<BookingSeat> bookingSeats)
        {
            await _context.BookingSeats.AddRangeAsync(bookingSeats);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking> CreateBookingWithSeatsAsync(
            Booking booking,
            List<BookingSeat> bookingSeats,
            List<ShowSeat> showSeats)
        {
            using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                // Step 1: Save the booking
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();

                // Step 2: Assign the generated BookingId
                foreach (var bookingSeat in bookingSeats)
                {
                    bookingSeat.BookingId = booking.BookingId;
                }

                // Step 3: Save booking seats
                await _context.BookingSeats.AddRangeAsync(bookingSeats);

                // Step 4: Update ShowSeat statuses
                _context.ShowSeats.UpdateRange(showSeats);

                // Step 5: Save all remaining changes
                await _context.SaveChangesAsync();

                // Step 6: Confirm transaction
                await transaction.CommitAsync();

                return booking;
            }
            catch
            {
                // Undo all changes if an error occurs
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task CancelBookingWithSeatsAsync(
            Booking booking,
            List<ShowSeat> showSeats)
        {
            using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                // Step 1: Change booking status
                booking.Status = "Cancelled";

                _context.Bookings.Update(booking);

                // Step 2: Make all booked seats available
                foreach (var showSeat in showSeats)
                {
                    showSeat.Status = "Available";
                }

                _context.ShowSeats.UpdateRange(showSeats);

                // Step 3: Save changes
                await _context.SaveChangesAsync();

                // Step 4: Confirm transaction
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


    }
}
