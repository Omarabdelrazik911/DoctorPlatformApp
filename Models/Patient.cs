using SQLite;
using System.ComponentModel.DataAnnotations;

namespace DoctorDashboardApp.Models
{
    public class Patient
    {
        [PrimaryKey, AutoIncrement]
        [Key]
        public int Id { get; set; }
        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;
        public string CurrentMedications { get; set; } = string.Empty;
        public string InsuranceProvider { get; set; } = string.Empty;
        public string InsuranceNumber { get; set; } = string.Empty;
        public string EmergencyContact { get; set; } = string.Empty;
        public string EmergencyContactPhone { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;
        public DateTime LastVisit { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
} 