namespace SmartAppointments.Api.Dtos
{
public class BookAppointmentDto
{
    public int ProviderId { get; set; }
    public required DateTime Date { get; set; }  // use 'required'
    public required string TimeSlot { get; set; }
}
}
