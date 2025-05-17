using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoctorDashboardApp.Models;
using DoctorDashboardApp.Services;

namespace DoctorDashboardApp.ViewModels
{
    public partial class PatientDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IDatabase _databaseService;

        [ObservableProperty]
        private Patient? patient;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private bool isNew;

        public PatientDetailsViewModel(IDatabase databaseService)
        {
            _databaseService = databaseService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Patient"))
            {
                Patient = query["Patient"] as Patient;
                IsEditing = false;
            }
            else
            {
                Patient = new Patient
                {
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    Gender = "Not Specified",
                    PhoneNumber = string.Empty,
                    Email = string.Empty,
                    Address = string.Empty,
                    MedicalHistory = string.Empty,
                    Allergies = string.Empty,
                    CurrentMedications = string.Empty,
                    InsuranceProvider = string.Empty,
                    InsuranceNumber = string.Empty,
                    EmergencyContact = string.Empty,
                    EmergencyContactPhone = string.Empty,
                    BloodType = "Unknown",
                    IsActive = true,
                    LastVisit = DateTime.Now
                };
                IsEditing = true;
                IsNew = true;
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            if (Patient == null) return;

            try
            {
                if (IsNew)
                {
                    await _databaseService.CreatePatientAsync(Patient);
                }
                else
                {
                    await _databaseService.UpdatePatientAsync(Patient);
                }
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to save patient.", "OK");
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            if (Patient == null) return;

            var confirm = await Shell.Current.DisplayAlert(
                "Confirm Delete",
                "Are you sure you want to delete this patient?",
                "Yes",
                "No");

            if (confirm)
            {
                await _databaseService.DeletePatientAsync(Patient.Id);
                await Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        private void Edit()
        {
            IsEditing = true;
        }

        [RelayCommand]
        private void Cancel()
        {
            if (IsNew)
            {
                Shell.Current.GoToAsync("..");
            }
            else
            {
                IsEditing = false;
            }
        }

        [RelayCommand]
        private async Task NewAppointment()
        {
            if (Patient == null) return;
            var parameters = new Dictionary<string, object>
            {
                { "Patient", Patient }
            };
            await Shell.Current.GoToAsync("NewAppointment", parameters);
        }

        [RelayCommand]
        private async Task NewOrder()
        {
            if (Patient == null) return;
            var parameters = new Dictionary<string, object>
            {
                { "Patient", Patient }
            };
            await Shell.Current.GoToAsync("NewOrder", parameters);
        }

        [RelayCommand]
        private async Task ExportRecords()
        {
            if (Patient == null) return;
            try
            {
                // TODO: Implement PDF export functionality
                await Shell.Current.DisplayAlert("Export", "Records export functionality coming soon!", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to export records.", "OK");
            }
        }
    }
} 