using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAppointments.Api.Data;
using SmartAppointments.Api.Models;
using SmartAppointments.Api.Dtos;
using System.Security.Claims;

namespace SmartAppointments.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // JWT required
    public class ReviewsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ReviewsController(AppDbContext db)
        {
            _db = db;
        }

        // ======================================
        // Helper: Get user ID from JWT token
        // ======================================
        private int GetUserIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(claim))
                throw new UnauthorizedAccessException("User ID not found in token");

            if (!int.TryParse(claim, out int userId))
                throw new UnauthorizedAccessException($"Invalid user ID: {claim}");

            return userId;
        }

        // ======================================
        // 1️⃣ Add a Review
        // ======================================
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto dto)
        {
            if (dto == null || dto.Rating < 1 || dto.Rating > 5)
                return BadRequest(new { message = "Invalid review data" });

            // Optional: enforce using token’s user ID instead of body
            int clientId = dto.ClientId;
            try
            {
                clientId = GetUserIdFromToken();
            }
            catch
            {
                // fallback to dto.ClientId if not authenticated
            }

            var providerExists = await _db.Providers.AnyAsync(p => p.Id == dto.ProviderId);
            if (!providerExists)
                return NotFound(new { message = "Provider not found" });

            var clientExists = await _db.Users.AnyAsync(u => u.Id == clientId);
            if (!clientExists)
                return NotFound(new { message = "Client not found" });

            var review = new Review
            {
                ProviderId = dto.ProviderId,
                ClientId = clientId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Review added successfully", review });
        }

        // ======================================
        // 2️⃣ Get Reviews for a Provider
        // ======================================
        [HttpGet("provider/{providerId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReviewsByProvider(int providerId)
        {
            var reviews = await _db.Reviews
                .Where(r => r.ProviderId == providerId)
                .Include(r => r.Client)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(reviews);
        }

        // ======================================
        // 3️⃣ Get Logged-in User's Reviews
        // ======================================
        [HttpGet("my")]
        public async Task<IActionResult> GetMyReviews()
        {
            int userId = GetUserIdFromToken();

            var reviews = await _db.Reviews
                .Where(r => r.ClientId == userId)
                .Include(r => r.Provider)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(reviews);
        }

        // ======================================
        // 4️⃣ Delete Review (Owner or Admin)
        // ======================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            int userId = GetUserIdFromToken();

            var review = await _db.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review == null)
                return NotFound(new { message = "Review not found" });

            if (review.ClientId != userId && !User.IsInRole("Admin"))
                return Forbid();

            _db.Reviews.Remove(review);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Review deleted successfully" });
        }
    }
}
