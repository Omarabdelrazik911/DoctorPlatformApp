namespace DoctorDashboardApp.Models;

public class Appointment
{
    public int Id { get; set; }
    public string PatientName { get; set; }
    public int PatientId { get; set; }
    public DateTime DateTime { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public string? Notes { get; set; }
    public int? DurationMinutes { get; set; }
    public bool IsRecurring { get; set; }
} 