using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoctorDashboardApp.Models;
using DoctorDashboardApp.Services;

namespace DoctorDashboardApp.ViewModels
{
    public partial class PatientsViewModel : ObservableObject
    {
        private readonly IDatabase _databaseService;

        [ObservableProperty]
        private ObservableCollection<Patient> patients;

        [ObservableProperty]
        private Patient? selectedPatient;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool isLoading;

        public PatientsViewModel(IDatabase databaseService)
        {
            _databaseService = databaseService;
            Patients = new ObservableCollection<Patient>();
            LoadPatients();
        }

        [RelayCommand]
        private async Task LoadPatients()
        {
            if (IsLoading) return;

            try
            {
                IsLoading = true;
                var allPatients = await _databaseService.GetAllPatientsAsync();
                Patients.Clear();
                foreach (var patient in allPatients)
                {
                    Patients.Add(patient);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to load patients.", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task SearchPatients()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadPatients();
                return;
            }

            try
            {
                IsLoading = true;
                var searchResults = await _databaseService.SearchPatientsAsync(SearchText);
                Patients.Clear();
                foreach (var patient in searchResults)
                {
                    Patients.Add(patient);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to search patients.", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task AddPatient()
        {
            await Shell.Current.GoToAsync("NewPatient");
        }

        [RelayCommand]
        private async Task ViewPatient(Patient patient)
        {
            if (patient == null) return;
            var parameters = new Dictionary<string, object>
            {
                { "Patient", patient }
            };
            await Shell.Current.GoToAsync("PatientDetails", parameters);
        }

        [RelayCommand]
        private async Task EditPatient(Patient patient)
        {
            if (patient == null) return;
            var parameters = new Dictionary<string, object>
            {
                { "Patient", patient }
            };
            await Shell.Current.GoToAsync("EditPatient", parameters);
        }

        [RelayCommand]
        private async Task Refresh()
        {
            await LoadPatients();
        }

        [RelayCommand]
        private async Task PatientSelected()
        {
            if (SelectedPatient == null) return;
            await ViewPatient(SelectedPatient);
            SelectedPatient = null;
        }
    }
} 