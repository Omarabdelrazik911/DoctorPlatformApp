using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoctorDashboardApp.Models;
using DoctorDashboardApp.Services;

namespace DoctorDashboardApp.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IDatabase _databaseService;
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string welcomeMessage = "Welcome back!";

        [ObservableProperty]
        private string currentDate = DateTime.Now.ToString("D");

        [ObservableProperty]
        private int totalPatients;

        [ObservableProperty]
        private int todayAppointments;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private int todayAppointmentsCount;

        [ObservableProperty]
        private int pendingOrdersCount;

        [ObservableProperty]
        private int newTestResultsCount;

        [ObservableProperty]
        private ObservableCollection<Patient> recentPatients;

        [ObservableProperty]
        private Doctor? currentDoctor;

        public DashboardViewModel(IDatabase databaseService, IAuthService authService)
        {
            _databaseService = databaseService;
            _authService = authService;
            RecentPatients = new ObservableCollection<Patient>();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            Task.Run(async () => await LoadDashboardDataAsync());
        }

        [RelayCommand]
        private async Task LoadDashboardDataAsync()
        {
            try
            {
                IsLoading = true;
                var currentUser = await _authService.GetCurrentUserAsync();
                if (currentUser != null)
                {
                    WelcomeMessage = $"Welcome back, Dr. {currentUser.LastName}!";
                }

                var patients = await _databaseService.GetAllPatientsAsync();
                TotalPatients = patients.Count;

                // For demo purposes, just show a random number of appointments
                var random = new Random();
                TodayAppointments = random.Next(1, 10);

                // TODO: Implement these methods in respective services
                TodayAppointmentsCount = 5; // await _appointmentService.GetTodayAppointmentsCountAsync();
                PendingOrdersCount = 3; // await _orderService.GetPendingOrdersCountAsync();
                NewTestResultsCount = 2; // await _testResultService.GetNewResultsCountAsync();

                // Load recent patients
                var recentPatients = await _databaseService.GetRecentPatientsAsync(5);
                RecentPatients.Clear();
                foreach (var patient in recentPatients)
                {
                    RecentPatients.Add(patient);
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task Logout()
        {
            await _authService.LogoutAsync();
            await Shell.Current.GoToAsync("///Login");
        }

        [RelayCommand]
        private async Task ViewAppointments()
        {
            await Shell.Current.GoToAsync("//AppointmentsPage");
        }

        [RelayCommand]
        private async Task ViewOrders()
        {
            await Shell.Current.GoToAsync("//OrdersPage");
        }

        [RelayCommand]
        private async Task ViewTestResults()
        {
            await Shell.Current.GoToAsync("//TestResultsPage");
        }

        [RelayCommand]
        private async Task NewPatient()
        {
            await Shell.Current.GoToAsync("//PatientsPage/NewPatient");
        }

        [RelayCommand]
        private async Task NewAppointment()
        {
            await Shell.Current.GoToAsync("//AppointmentsPage/NewAppointment");
        }

        [RelayCommand]
        private async Task NewOrder()
        {
            await Shell.Current.GoToAsync("//OrdersPage/NewOrder");
        }
    }
} 