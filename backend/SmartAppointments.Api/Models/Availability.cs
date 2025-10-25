namespace SmartAppointments.Api.Models
{
    public class Availability
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public DateTime Date { get; set; }
        public string TimeSlot { get; set; } = string.Empty;

        public Provider? Provider { get; set; }
    }
}
