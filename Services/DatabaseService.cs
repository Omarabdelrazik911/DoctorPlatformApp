using System.Threading.Tasks;
using SQLite;
using DoctorDashboardApp.Models;

namespace DoctorDashboardApp.Services
{
    public class DatabaseService : IDatabase
    {
        private SQLiteAsyncConnection _database;
        private readonly string _databasePath;

        public DatabaseService()
        {
            _databasePath = Path.Combine(FileSystem.AppDataDirectory, "doctordashboard.db");
        }

        public async Task InitializeDatabaseAsync()
        {
            if (_database != null)
                return;

            _database = new SQLiteAsyncConnection(_databasePath);
            await _database.CreateTableAsync<Patient>();
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            await InitializeDatabaseAsync();
            return await _database.Table<Patient>().ToListAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            await InitializeDatabaseAsync();
            return await _database.Table<Patient>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> AddPatientAsync(Patient patient)
        {
            await InitializeDatabaseAsync();
            var result = await _database.InsertAsync(patient);
            return result > 0;
        }

        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            await InitializeDatabaseAsync();
            var result = await _database.UpdateAsync(patient);
            return result > 0;
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            await InitializeDatabaseAsync();
            var result = await _database.DeleteAsync<Patient>(id);
            return result > 0;
        }

        public async Task<Doctor?> GetDoctorByCredentialsAsync(string username, string passwordHash)
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return null;

            return await _database.Table<Doctor>()
                .Where(d => d.Username == username && d.PasswordHash == passwordHash && d.IsActive)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateDoctorAsync(Doctor doctor)
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return false;

            var result = await _database.UpdateAsync(doctor);
            return result > 0;
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync()
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return new List<Doctor>();

            return await _database.Table<Doctor>().ToListAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return null;

            return await _database.Table<Doctor>()
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return false;

            var result = await _database.DeleteAsync<Doctor>(id);
            return result > 0;
        }

        public async Task<bool> CreateDoctorAsync(Doctor doctor)
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return false;

            var result = await _database.InsertAsync(doctor);
            return result > 0;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return new List<Patient>();

            return await _database.Table<Patient>()
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.LastVisit)
                .ToListAsync();
        }

        public async Task<List<Patient>> GetRecentPatientsAsync(int count = 5)
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return new List<Patient>();

            return await _database.Table<Patient>()
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.LastVisit)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Patient>> SearchPatientsAsync(string searchTerm)
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return new List<Patient>();

            return await _database.Table<Patient>()
                .Where(p => p.IsActive &&
                    ((p.FirstName ?? "").Contains(searchTerm) ||
                     (p.LastName ?? "").Contains(searchTerm) ||
                     (p.PhoneNumber ?? "").Contains(searchTerm) ||
                     (p.Email ?? "").Contains(searchTerm)))
                .OrderByDescending(p => p.LastVisit)
                .ToListAsync();
        }

        public async Task<Patient> GetPatientAsync(int id)
        {
            await InitializeDatabaseAsync();
            return await _database.Table<Patient>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SavePatientAsync(Patient patient)
        {
            await InitializeDatabaseAsync();
            if (patient.Id != 0)
                return await _database.UpdateAsync(patient);
            return await _database.InsertAsync(patient);
        }

        public async Task<List<Appointment>> GetAppointmentsAsync(DateTime? date = null)
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return new List<Appointment>();

            if (date.HasValue)
            {
                var startDate = date.Value.Date;
                var endDate = startDate.AddDays(1);
                return await _database.Table<Appointment>()
                    .Where(a => a.DateTime >= startDate && a.DateTime < endDate)
                    .OrderBy(a => a.DateTime)
                    .ToListAsync();
            }
            return await _database.Table<Appointment>().OrderBy(a => a.DateTime).ToListAsync();
        }

        public async Task<int> SaveAppointmentAsync(Appointment appointment)
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return -1;

            if (appointment.Id != 0)
                return await _database.UpdateAsync(appointment);
            return await _database.InsertAsync(appointment);
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return new List<Order>();

            return await _database.Table<Order>().OrderByDescending(o => o.OrderDate).ToListAsync();
        }

        public async Task<int> SaveOrderAsync(Order order)
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return -1;

            if (order.Id != 0)
                return await _database.UpdateAsync(order);
            return await _database.InsertAsync(order);
        }

        public async Task<List<TestResult>> GetTestResultsAsync()
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return new List<TestResult>();

            return await _database.Table<TestResult>().OrderByDescending(t => t.TestDate).ToListAsync();
        }

        public async Task<int> SaveTestResultAsync(TestResult testResult)
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return -1;

            if (testResult.Id != 0)
                return await _database.UpdateAsync(testResult);
            return await _database.InsertAsync(testResult);
        }

        public async Task<List<Document>> GetDocumentsAsync()
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return new List<Document>();

            return await _database.Table<Document>().OrderByDescending(d => d.Date).ToListAsync();
        }

        public async Task<int> SaveDocumentAsync(Document document)
        {
            await InitializeDatabaseAsync();
            if (_database == null)
                return -1;

            if (document.Id != 0)
                return await _database.UpdateAsync(document);
            return await _database.InsertAsync(document);
        }
    }
}