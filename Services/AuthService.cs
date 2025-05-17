using System.Security.Cryptography;
using System.Text;
using DoctorDashboardApp.Models;
using Microsoft.Maui.Storage;

namespace DoctorDashboardApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly ISecureStorage _secureStorage;
        private readonly IDatabase _database;
        private Doctor? _currentUser;
        private const string AuthTokenKey = "auth_token";

        public AuthService(ISecureStorage secureStorage, IDatabase database)
        {
            _secureStorage = secureStorage;
            _database = database;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            // TODO: Implement actual authentication logic
            if (username == "admin" && password == "password")
            {
                await _secureStorage.SetAsync(AuthTokenKey, "dummy_token");
                _currentUser = new Doctor
                {
                    Id = 1,
                    Username = username,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@example.com",
                    IsActive = true
                };
                return true;
            }
            return false;
        }

        public async Task<bool> LogoutAsync()
        {
            _currentUser = null;
            await _secureStorage.Remove(AuthTokenKey);
            return true;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await _secureStorage.GetAsync(AuthTokenKey);
            return !string.IsNullOrEmpty(token);
        }

        public async Task<Doctor?> GetCurrentUserAsync()
        {
            if (_currentUser != null)
                return _currentUser;

            var token = await _secureStorage.GetAsync(AuthTokenKey);
            if (string.IsNullOrEmpty(token))
                return null;

            // In a real app, you would validate the token and fetch the user from the database
            _currentUser = new Doctor
            {
                Id = 1,
                Username = "admin",
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com",
                IsActive = true
            };

            return _currentUser;
        }

        public async Task<bool> ChangePasswordAsync(string oldPassword, string newPassword)
        {
            if (_currentUser == null) return false;

            var oldHash = HashPassword(oldPassword);
            if (_currentUser.PasswordHash != oldHash) return false;

            _currentUser.PasswordHash = HashPassword(newPassword);
            return await _database.UpdateDoctorAsync(_currentUser);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
} 