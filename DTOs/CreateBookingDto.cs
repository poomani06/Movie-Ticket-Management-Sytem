
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs
{
    public class CreateBookingDto
    {
        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string CustomerPhone { get; set; } = string.Empty;

        [Required]
        public int ShowId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Select at least one seat.")]
        public List<int> SeatIds { get; set; } = new List<int>();
    }
}
