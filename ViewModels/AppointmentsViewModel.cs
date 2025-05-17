using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoctorDashboardApp.Models;
using DoctorDashboardApp.Services;

namespace DoctorDashboardApp.ViewModels;

public partial class AppointmentsViewModel : ObservableObject
{
    private readonly IDatabase _databaseService;
    
    [ObservableProperty]
    private ObservableCollection<Appointment> appointments;
    
    [ObservableProperty]
    private bool isLoading;
    
    [ObservableProperty]
    private DateTime selectedDate = DateTime.Today;

    public AppointmentsViewModel(IDatabase databaseService)
    {
        _databaseService = databaseService;
        Appointments = new ObservableCollection<Appointment>();
        LoadAppointments();
    }

    [RelayCommand]
    private async Task LoadAppointments()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            // TODO: Implement appointment loading from database
            // For now, add sample data
            Appointments.Clear();
            Appointments.Add(new Appointment 
            { 
                PatientName = "John Doe",
                DateTime = DateTime.Now.AddHours(2),
                Type = "Check-up",
                Status = "Scheduled"
            });
            Appointments.Add(new Appointment 
            { 
                PatientName = "Jane Smith",
                DateTime = DateTime.Now.AddHours(4),
                Type = "Follow-up",
                Status = "Confirmed"
            });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task AddAppointment()
    {
        // TODO: Implement add appointment functionality
        await Shell.Current.DisplayAlert("Info", "Add appointment functionality coming soon", "OK");
    }

    partial void OnSelectedDateChanged(DateTime value)
    {
        LoadAppointments();
    }
} 