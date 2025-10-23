using SmartAppointments.Api.Models;

namespace SmartAppointments.Api.Services
{
    public interface ITokenService
{
    string CreateToken(User user);
}
}
