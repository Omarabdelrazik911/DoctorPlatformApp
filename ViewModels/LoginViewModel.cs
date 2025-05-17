using System.Windows.Input;
using DoctorDashboardApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DoctorDashboardApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string username = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string message = string.Empty;

        [ObservableProperty]
        private bool isLoading;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Please enter username and password";
                return;
            }

            try
            {
                IsLoading = true;
                var user = await _authService.LoginAsync(Username, Password);
                
                if (user != null)
                {
                    await Shell.Current.GoToAsync("///Dashboard");
                }
                else
                {
                    Message = "Invalid username or password";
                }
            }
            catch (Exception ex)
            {
                Message = "Login failed. Please try again.";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
} 