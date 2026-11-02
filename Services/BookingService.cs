
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories;

namespace MovieTicketBooking.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IShowRepository _showRepository;
        private readonly IUserRepository _userRepository;
        public BookingService(
    IBookingRepository bookingRepository,
    IShowRepository showRepository,
    IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _showRepository = showRepository;
            _userRepository = userRepository;
        }

        public async Task<Booking> CreateBookingAsync(
    int userId,
    int showId,
    List<int> seatIds)
        {
            // Step 1: Get the logged-in user
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
                throw new KeyNotFoundException("User not found.");

            // Step 2: Validate seat selection
            if (seatIds == null || seatIds.Count == 0)
                throw new ArgumentException("Select at least one seat.");

            if (seatIds.Distinct().Count() != seatIds.Count)
                throw new ArgumentException("Duplicate seats are not allowed.");

            // Step 3: Check whether the show exists
            var show = await _showRepository.GetShowByIdAsync(showId);

            if (show == null)
                throw new KeyNotFoundException("Show not found.");

            // Step 4: Check selected seats
            var selectedShowSeats = new List<ShowSeat>();

            foreach (var seatId in seatIds)
            {
                var showSeat = await _showRepository
                    .GetShowSeatByShowAndSeatAsync(showId, seatId);

                if (showSeat == null)
                    throw new ArgumentException(
                        $"Seat {seatId} does not belong to this show.");

                if (showSeat.Status != "Available")
                    throw new ArgumentException(
                        $"Seat {seatId} is not available.");

                selectedShowSeats.Add(showSeat);
            }

            // Step 5: Calculate total amount
            decimal totalAmount = seatIds.Count * show.TicketPrice;

            // Step 6: Create booking using registered user details
            var booking = new Booking
            {
                BookingReference = "BK" +
                    Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),

                ShowId = showId,
                UserId = user.UserId,
                CustomerName = user.Name,
                CustomerEmail = user.Email,
                CustomerPhone = user.Phone,
                BookingDate = DateTime.Now,
                TotalAmount = totalAmount,
                Status = "Confirmed"
            };

            // Step 7: Create BookingSeat records
            var bookingSeats = seatIds.Select(seatId => new BookingSeat
            {
                SeatId = seatId,
                Price = show.TicketPrice
            }).ToList();

            // Step 8: Change seat status
            foreach (var showSeat in selectedShowSeats)
            {
                showSeat.Status = "Booked";
            }

            // Step 9: Save everything using repository transaction
            return await _bookingRepository.CreateBookingWithSeatsAsync(
                booking,
                bookingSeats,
                selectedShowSeats);
        }
        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllBookingsAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _bookingRepository.GetBookingByIdAsync(id);
        }

        public async Task<Booking?> GetBookingByReferenceAsync(string reference)
        {
            return await _bookingRepository
                .GetBookingByReferenceAsync(reference);
        }


        public async Task CancelBookingAsync(int bookingId)
        {
            // Step 1: Find the booking
            var booking = await _bookingRepository
                .GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found.");
            }

            // Step 2: Check booking status
            if (booking.Status == "Cancelled")
            {
                throw new ArgumentException(
                    "Booking is already cancelled.");
            }

            // Step 3: Get seats belonging to this booking
            var bookingSeats = await _bookingRepository
                .GetBookingSeatsAsync(bookingId);

            // Step 4: Find corresponding ShowSeats
            var showSeats = new List<ShowSeat>();

            foreach (var bookingSeat in bookingSeats)
            {
                var showSeat = await _showRepository
                    .GetShowSeatByShowAndSeatAsync(
                        booking.ShowId,
                        bookingSeat.SeatId);

                if (showSeat != null)
                {
                    showSeats.Add(showSeat);
                }
            }

            // Step 5: Cancel booking and release seats
            await _bookingRepository.CancelBookingWithSeatsAsync(
                booking,
                showSeats);
        }

    }
}
