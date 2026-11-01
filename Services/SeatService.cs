using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories;

namespace MovieTicketBooking.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IScreenRepository _screenRepository;

        public SeatService(
            ISeatRepository seatRepository,
            IScreenRepository screenRepository)
        {
            _seatRepository = seatRepository;
            _screenRepository = screenRepository;
        }

        public async Task<Seat> AddSeatAsync(Seat seat)
        {
            ValidateSeat(seat);

            var screen = await _screenRepository.GetScreenByIdAsync(seat.ScreenId);

            if (screen == null)
            {
                throw new KeyNotFoundException("Screen not found.");
            }

            return await _seatRepository.AddSeatAsync(seat);
        }

        public async Task<List<Seat>> AddSeatsAsync(List<Seat> seats)
        {
            if (seats == null || seats.Count == 0)
            {
                throw new ArgumentException("At least one seat is required.");
            }

            foreach (var seat in seats)
            {
                ValidateSeat(seat);

                var screen = await _screenRepository.GetScreenByIdAsync(seat.ScreenId);

                if (screen == null)
                {
                    throw new KeyNotFoundException("Screen not found.");
                }
            }

            return await _seatRepository.AddSeatsAsync(seats);
        }

        public async Task<List<Seat>> GetSeatsByScreenIdAsync(int screenId)
        {
            var screen = await _screenRepository.GetScreenByIdAsync(screenId);

            if (screen == null)
            {
                throw new KeyNotFoundException("Screen not found.");
            }

            return await _seatRepository.GetSeatsByScreenIdAsync(screenId);
        }

        public async Task<Seat?> GetSeatByIdAsync(int id)
        {
            return await _seatRepository.GetSeatByIdAsync(id);
        }

        public async Task UpdateSeatAsync(int id, Seat seat)
        {
            ValidateSeat(seat);

            var existingSeat = await _seatRepository.GetSeatByIdAsync(id);

            if (existingSeat == null)
            {
                throw new KeyNotFoundException("Seat not found.");
            }

            var screen = await _screenRepository.GetScreenByIdAsync(seat.ScreenId);

            if (screen == null)
            {
                throw new KeyNotFoundException("Screen not found.");
            }

            existingSeat.ScreenId = seat.ScreenId;
            existingSeat.RowName = seat.RowName;
            existingSeat.SeatNumber = seat.SeatNumber;
            existingSeat.SeatType = seat.SeatType;

            await _seatRepository.UpdateSeatAsync(existingSeat);
        }

        public async Task DeleteSeatAsync(int id)
        {
            var seat = await _seatRepository.GetSeatByIdAsync(id);

            if (seat == null)
            {
                throw new KeyNotFoundException("Seat not found.");
            }

            await _seatRepository.DeleteSeatAsync(seat);
        }

        private void ValidateSeat(Seat seat)
        {
            if (string.IsNullOrWhiteSpace(seat.RowName))
            {
                throw new ArgumentException("Row name is required.");
            }

            if (seat.SeatNumber <= 0)
            {
                throw new ArgumentException("Seat number must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(seat.SeatType))
            {
                throw new ArgumentException("Seat type is required.");
            }
        }
    }
}