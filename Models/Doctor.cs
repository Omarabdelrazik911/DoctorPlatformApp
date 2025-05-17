using System;
using System.ComponentModel.DataAnnotations;
using SQLite;

namespace DoctorDashboardApp.Models
{
    public class Doctor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime LastLoginDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
} 