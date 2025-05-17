namespace DoctorDashboardApp.Models;

public class Document
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public string? FilePath { get; set; }
    public int? PatientId { get; set; }
    public string? Notes { get; set; }
} 