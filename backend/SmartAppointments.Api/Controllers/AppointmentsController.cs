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
    [Authorize] // Protects all routes with JWT
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AppointmentsController(AppDbContext db)
        {
            _db = db;
        }

        // ================================
        // Helper: safely get numeric user ID from JWT
        // ================================
        private int GetUserIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(claim))
                throw new UnauthorizedAccessException("User ID claim not found in token");

            if (!int.TryParse(claim, out int userId))
                throw new UnauthorizedAccessException($"Invalid user ID in token: {claim}");

            return userId;
        }

        // ================================
        // 1. Create Appointment
        // ================================
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] BookAppointmentDto dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Invalid appointment details" });

            if (dto.ProviderId <= 0)
                return BadRequest(new { message = "ProviderId is required" });

            if (string.IsNullOrEmpty(dto.TimeSlot))
                return BadRequest(new { message = "TimeSlot is required" });

            int userId;
            try
            {
                userId = GetUserIdFromToken();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }

            var appointment = new Appointment
            {
                ClientId = userId,
                ProviderId = dto.ProviderId,
                Date = DateOnly.FromDateTime(dto.Date),
                TimeSlot = dto.TimeSlot,
                Status = "Pending"
            };

            _db.Appointments.Add(appointment);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Appointment created successfully", appointment });
        }

        // ================================
        // 2. Get all appointments (Admin only)
        // ================================
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _db.Appointments
                .Include(a => a.Client)
                .Include(a => a.Provider)
                .ToListAsync();

            return Ok(appointments);
        }

        // ================================
        // 3. Get logged-in user’s appointments
        // ================================
        [HttpGet("my")]
        public async Task<IActionResult> GetMyAppointments()
        {
            int userId;
            try
            {
                userId = GetUserIdFromToken();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }

            var appointments = await _db.Appointments
                .Where(a => a.ClientId == userId)
                .Include(a => a.Provider)
                .ToListAsync();

            return Ok(appointments);
        }

        // ================================
        // 4. Cancel an appointment
        // ================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            int userId;
            try
            {
                userId = GetUserIdFromToken();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }

            var appointment = await _db.Appointments
                .FirstOrDefaultAsync(a => a.Id == id && a.ClientId == userId);

            if (appointment == null)
                return NotFound(new { message = "Appointment not found or does not belong to you" });

            appointment.Status = "Cancelled";
            await _db.SaveChangesAsync();

            return Ok(new { message = "Appointment cancelled successfully", appointment });
        }

        [HttpGet("{id}")]
public async Task<IActionResult> GetAppointment(int id)
{
    int userId = GetUserIdFromToken();

    var appointment = await _db.Appointments
        .Include(a => a.Client)
        .Include(a => a.Provider)
        .FirstOrDefaultAsync(a => a.Id == id);

    if (appointment == null)
        return NotFound(new { message = "Appointment not found" });

    // Check if user is owner or admin
    if (appointment.ClientId != userId && !User.IsInRole("Admin"))
        return Forbid();

    return Ok(appointment);
}

[HttpPut("{id}")]
public async Task<IActionResult> UpdateAppointment(int id, [FromBody] BookAppointmentDto dto)
{
    int userId = GetUserIdFromToken();

    var appointment = await _db.Appointments.FirstOrDefaultAsync(a => a.Id == id);
    if (appointment == null)
        return NotFound(new { message = "Appointment not found" });

    // Only owner or admin can update
    if (appointment.ClientId != userId && !User.IsInRole("Admin"))
        return Forbid();

    if (dto.ProviderId > 0) appointment.ProviderId = dto.ProviderId;
    if (!string.IsNullOrEmpty(dto.TimeSlot)) appointment.TimeSlot = dto.TimeSlot;
    appointment.Date = DateOnly.FromDateTime(dto.Date);

    await _db.SaveChangesAsync();
    return Ok(new { message = "Appointment updated", appointment });
}

    }
}
