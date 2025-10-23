namespace SmartAppointments.Api.Models
{
    public class Provider
{
    public int Id { get; set; }
    public int UserId { get; set; }        // FK to User
    public User? User { get; set; }
    public string Category { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public decimal Rating { get; set; } = 0m;
}
}
