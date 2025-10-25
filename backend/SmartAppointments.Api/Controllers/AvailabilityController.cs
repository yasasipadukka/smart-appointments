using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAppointments.Api.Data;
using SmartAppointments.Api.Models;
using System.Security.Claims;

namespace SmartAppointments.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AvailabilityController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AvailabilityController(AppDbContext db)
        {
            _db = db;
        }

        private int GetUserIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(claim))
                throw new UnauthorizedAccessException("User ID not found in token");

            if (!int.TryParse(claim, out int userId))
                throw new UnauthorizedAccessException("Invalid user ID format");

            return userId;
        }

        // ============================
        // 1. Add new availability
        // ============================
        [HttpPost]
        [Authorize(Roles = "Provider,Admin")]
        public async Task<IActionResult> AddAvailability([FromBody] Availability availability)
        {
            if (availability == null)
                return BadRequest(new { message = "Invalid data" });

            int providerId = GetUserIdFromToken();
            availability.ProviderId = providerId;

            _db.Availabilities.Add(availability);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Availability added", availability });
        }

        // ============================
        // 2. Get provider’s availability
        // ============================
        [HttpGet("provider/{providerId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProviderAvailability(int providerId)
        {
            var slots = await _db.Availabilities
                .Where(a => a.ProviderId == providerId)
                .ToListAsync();

            return Ok(slots);
        }

        // ============================
        // 3. Delete availability
        // ============================
        [HttpDelete("{id}")]
        [Authorize(Roles = "Provider,Admin")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            int providerId = GetUserIdFromToken();

            var availability = await _db.Availabilities
                .FirstOrDefaultAsync(a => a.Id == id && a.ProviderId == providerId);

            if (availability == null)
                return NotFound(new { message = "Slot not found or not yours" });

            _db.Availabilities.Remove(availability);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Slot deleted successfully" });
        }
    }
}
