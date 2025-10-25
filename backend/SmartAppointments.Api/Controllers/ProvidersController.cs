using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAppointments.Api.Data;
using SmartAppointments.Api.Models;

namespace SmartAppointments.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProviderController(AppDbContext db)
        {
            _db = db;
        }

        // ============================
        // 1. Get all providers
        // ============================
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProviders()
        {
            var providers = await _db.Providers.ToListAsync();
            return Ok(providers);
        }

        // ============================
        // 2. Get provider by ID
        // ============================
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProvider(int id)
        {
            var provider = await _db.Providers.FindAsync(id);
            if (provider == null)
                return NotFound(new { message = "Provider not found" });

            return Ok(provider);
        }

        // ============================
        // 3. Add provider (Admin only)
        // ============================
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProvider([FromBody] Provider provider)
        {
            if (provider == null)
                return BadRequest(new { message = "Invalid data" });

            _db.Providers.Add(provider);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Provider added successfully", provider });
        }

        // ============================
        // 4. Update provider (Admin only)
        // ============================
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProvider(int id, [FromBody] Provider updated)
        {
            var provider = await _db.Providers.FindAsync(id);
            if (provider == null)
                return NotFound(new { message = "Provider not found" });

            provider.Name = updated.Name;
            provider.Specialty = updated.Specialty;
            provider.ContactInfo = updated.ContactInfo;
            provider.Description = updated.Description;

            await _db.SaveChangesAsync();
            return Ok(new { message = "Provider updated", provider });
        }

        // ============================
        // 5. Delete provider (Admin only)
        // ============================
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var provider = await _db.Providers.FindAsync(id);
            if (provider == null)
                return NotFound(new { message = "Provider not found" });

            _db.Providers.Remove(provider);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Provider deleted successfully" });
        }
    }
}
