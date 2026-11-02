namespace MovieTicketBooking.DTOs
{
    public class CreateScreenDto
    {
        public int TheatreId { get; set; }

        public string ScreenName { get; set; } = string.Empty;
    }
}