using Microsoft.Maui.Controls;
using DoctorDashboardApp.Services;

namespace DoctorDashboardApp.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly IAuthService _authService;

        public LoginPage(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                var username = UsernameEntry.Text;
                var password = PasswordEntry.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageLabel.Text = "Please enter username and password";
                    MessageLabel.TextColor = Colors.Red;
                    return;
                }

                var result = await _authService.LoginAsync(username, password);
                if (result)
                {
                    // Navigate to main dashboard
                    await Shell.Current.GoToAsync("//MainTabs");
                }
                else
                {
                    MessageLabel.Text = "Invalid username or password";
                    MessageLabel.TextColor = Colors.Red;
                }
            }
            catch (Exception ex)
            {
                MessageLabel.Text = "An error occurred. Please try again.";
                MessageLabel.TextColor = Colors.Red;
            }
        }
    }
}