namespace SmartAppointments.Api.Dtos
{
    public class ReviewDto
    {
        public int ProviderId { get; set; }
        public int ClientId { get; set; } // You can also get this from JWT later
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
