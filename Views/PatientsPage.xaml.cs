using Microsoft.Maui.Controls;
using DoctorDashboardApp.Services;

namespace DoctorDashboardApp.Views
{
    public partial class PatientsPage : ContentPage
    {
        private readonly IDatabase _databaseService;

        public PatientsPage(IDatabase databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            LoadPatients();
        }

        private async void LoadPatients()
        {
            try
            {
                var patients = await _databaseService.GetPatientsAsync();
                PatientsCollection.ItemsSource = patients;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to load patients.", "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadPatients();
        }
    }
} 