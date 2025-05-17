namespace DoctorDashboardApp.Models;

public class TestResult
{
    public int Id { get; set; }
    public string PatientName { get; set; }
    public int PatientId { get; set; }
    public string TestName { get; set; }
    public string Result { get; set; }
    public string Status { get; set; }
    public DateTime TestDate { get; set; }
    public string? Notes { get; set; }
    public string? ReferenceRange { get; set; }
    public string? Units { get; set; }
    public string? PerformedBy { get; set; }
    public bool IsAbnormal { get; set; }
} 