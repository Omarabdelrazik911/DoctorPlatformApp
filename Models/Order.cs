namespace DoctorDashboardApp.Models;

public class Order
{
    public int Id { get; set; }
    public string PatientName { get; set; }
    public int PatientId { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public string Details { get; set; }
    public string? Notes { get; set; }
    public int? Priority { get; set; }
    public DateTime? CompletionDate { get; set; }
} 