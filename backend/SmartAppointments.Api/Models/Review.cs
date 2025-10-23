namespace SmartAppointments.Api.Models
{
public class Review
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }
    public int ClientId { get; set; }
    public User? Client { get; set; }
    public int Rating { get; set; }    // 1-5
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
}
