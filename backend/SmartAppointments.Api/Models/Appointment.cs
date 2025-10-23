namespace SmartAppointments.Api.Models
{
    public class Appointment
{
    public int Id { get; set; }
    public int ClientId { get; set; }      // FK to User (Client)
    public User? Client { get; set; }
    public int ProviderId { get; set; }    // FK to Provider
    public Provider? Provider { get; set; }
    public DateOnly Date { get; set; }
    public string TimeSlot { get; set; } = null!; // e.g. "10:00-10:30"
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Completed, Cancelled
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

}