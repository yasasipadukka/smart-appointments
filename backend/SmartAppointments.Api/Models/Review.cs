using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAppointments.Api.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProviderId { get; set; }

        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; } = null!;

        [Required]
        public int ClientId { get; set; }  // 👈 The client (user) who gave the review

        [ForeignKey("ClientId")]
        public User Client { get; set; } = null!; // 👈 Link to User model

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // 👈 auto timestamp
    }
}
