using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs
{
    public class CreateBookingDto
    {
        [Required]
        public int ShowId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Select at least one seat.")]
        public List<int> SeatIds { get; set; } = new List<int>();
    }
}