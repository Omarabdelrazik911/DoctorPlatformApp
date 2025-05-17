using DoctorDashboardApp.Models;

namespace DoctorDashboardApp.Services
{
    public interface IDatabase
    {
        Task InitializeDatabaseAsync();
        Task<List<Patient>> GetPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int id);
        Task<bool> AddPatientAsync(Patient patient);
        Task<bool> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(int id);
        Task<Doctor?> GetDoctorByCredentialsAsync(string username, string passwordHash);
        Task<bool> UpdateDoctorAsync(Doctor doctor);
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(int id);
        Task<bool> DeleteDoctorAsync(int id);
        Task<bool> CreateDoctorAsync(Doctor doctor);

        // Patient-related methods
        Task<List<Patient>> GetAllPatientsAsync();
        Task<List<Patient>> GetRecentPatientsAsync(int count = 5);
        Task<List<Patient>> SearchPatientsAsync(string searchTerm);

        // Additional methods for other entities
        Task<List<Appointment>> GetAppointmentsAsync(DateTime? date = null);
        Task<int> SaveAppointmentAsync(Appointment appointment);
        Task<List<Order>> GetOrdersAsync();
        Task<int> SaveOrderAsync(Order order);
        Task<List<TestResult>> GetTestResultsAsync();
        Task<int> SaveTestResultAsync(TestResult testResult);
        Task<List<Document>> GetDocumentsAsync();
        Task<int> SaveDocumentAsync(Document document);
    }
} 