using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SmartAppointments.Api.Data;
using SmartAppointments.Api.Models;
using SmartAppointments.Api.Dtos;
using SmartAppointments.Api.Services;

namespace SmartAppointments.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvidersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProvidersController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var providers = await _db.Providers
                                     .Include(p => p.User) // Include related User
                                     .ToListAsync();
            return Ok(providers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var provider = await _db.Providers
                                    .Include(p => p.User)
                                    .SingleOrDefaultAsync(p => p.Id == id);

            if (provider == null)
                return NotFound(new { message = "Provider not found" });

            return Ok(provider);
        }

       [HttpPost]
[Authorize(Roles = "Provider,Admin")]
public async Task<IActionResult> Create([FromBody] Provider provider)
{
    if (provider == null)
        return BadRequest(new { message = "Invalid provider data" });

    // Get the logged-in user's ID from JWT claims
    var userIdClaim = User.FindFirst("id"); // "id" is what your token service sets
    if (userIdClaim == null)
        return Unauthorized();

    provider.UserId = int.Parse(userIdClaim.Value); // automatically assign the user ID

    _db.Providers.Add(provider);
    await _db.SaveChangesAsync();

    return CreatedAtAction(nameof(Get), new { id = provider.Id }, provider);
}

    }
}
