namespace SmartAppointments.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "Client"; // "Admin","Provider","Client"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}