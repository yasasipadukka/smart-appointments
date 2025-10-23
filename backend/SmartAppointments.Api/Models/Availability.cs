namespace SmartAppointments.Api.Models
{
    public class Availability
{
    public int Id { get; set; }
    public int ProviderId { get; set; }
    public Provider? Provider { get; set; }
    public DayOfWeek DayOfWeek { get; set; }    // Monday..Sunday
    public TimeOnly StartTime { get; set; }     // e.g. 09:00
    public TimeOnly EndTime { get; set; }       // e.g. 17:00
}

}