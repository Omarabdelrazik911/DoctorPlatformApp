using System.Threading.Tasks;
using DoctorDashboardApp.Models;

namespace DoctorDashboardApp.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<bool> LogoutAsync();
        Task<bool> IsAuthenticatedAsync();
        Task<Doctor?> GetCurrentUserAsync();
    }
} 