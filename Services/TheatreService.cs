using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories;

namespace MovieTicketBooking.Services
{
    public class TheatreService : ITheatreService
    {
        private readonly ITheatreRepository _theatreRepository;

        public TheatreService(ITheatreRepository theatreRepository)
        {
            _theatreRepository = theatreRepository;
        }

        // Add Theatre
        public async Task<Theatre> AddTheatreAsync(Theatre theatre)
        {
            if (string.IsNullOrWhiteSpace(theatre.Name))
            {
                throw new ArgumentException("Theatre name is required.");
            }

            if (string.IsNullOrWhiteSpace(theatre.Location))
            {
                throw new ArgumentException("Theatre location is required.");
            }

            return await _theatreRepository.AddTheatreAsync(theatre);
        }

        // Get All Theatres
        public async Task<List<Theatre>> GetAllTheatresAsync()
        {
            return await _theatreRepository.GetAllTheatresAsync();
        }

        // Get Theatre By ID
        public async Task<Theatre?> GetTheatreByIdAsync(int id)
        {
            return await _theatreRepository.GetTheatreByIdAsync(id);
        }

        // Update Theatre
        public async Task UpdateTheatreAsync(int id, Theatre theatre)
        {
            var existingTheatre =
                await _theatreRepository.GetTheatreByIdAsync(id);

            if (existingTheatre == null)
            {
                throw new KeyNotFoundException("Theatre not found.");
            }

            if (string.IsNullOrWhiteSpace(theatre.Name))
            {
                throw new ArgumentException("Theatre name is required.");
            }

            if (string.IsNullOrWhiteSpace(theatre.Location))
            {
                throw new ArgumentException("Theatre location is required.");
            }

            existingTheatre.Name = theatre.Name;
            existingTheatre.Location = theatre.Location;

            await _theatreRepository.UpdateTheatreAsync(existingTheatre);
        }

        // Delete Theatre
        public async Task DeleteTheatreAsync(int id)
        {
            var theatre =
                await _theatreRepository.GetTheatreByIdAsync(id);

            if (theatre == null)
            {
                throw new KeyNotFoundException("Theatre not found.");
            }

            await _theatreRepository.DeleteTheatreAsync(theatre);
        }
    }
}