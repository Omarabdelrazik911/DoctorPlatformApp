using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoctorDashboardApp.Models;
using DoctorDashboardApp.Services;

namespace DoctorDashboardApp.ViewModels;

public partial class OrdersViewModel : ObservableObject
{
    private readonly IDatabase _databaseService;
    
    [ObservableProperty]
    private ObservableCollection<Order> orders;
    
    [ObservableProperty]
    private bool isLoading;
    
    [ObservableProperty]
    private string searchQuery;

    public OrdersViewModel(IDatabase databaseService)
    {
        _databaseService = databaseService;
        Orders = new ObservableCollection<Order>();
        LoadOrders();
    }

    [RelayCommand]
    private async Task LoadOrders()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            // TODO: Implement order loading from database
            // For now, add sample data
            Orders.Clear();
            Orders.Add(new Order 
            { 
                PatientName = "John Doe",
                Type = "Medication",
                Status = "Pending",
                OrderDate = DateTime.Now.AddDays(-1),
                Details = "Amoxicillin 500mg"
            });
            Orders.Add(new Order 
            { 
                PatientName = "Jane Smith",
                Type = "Lab Test",
                Status = "Completed",
                OrderDate = DateTime.Now.AddDays(-2),
                Details = "Complete Blood Count"
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
    private async Task SearchOrders()
    {
        await LoadOrders();
        if (string.IsNullOrWhiteSpace(SearchQuery)) return;
        
        var filteredOrders = Orders.Where(o => 
            o.PatientName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
            o.Type.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
            o.Details.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();
            
        Orders.Clear();
        foreach (var order in filteredOrders)
        {
            Orders.Add(order);
        }
    }

    [RelayCommand]
    private async Task CreateOrder()
    {
        // TODO: Implement create order functionality
        await Shell.Current.DisplayAlert("Info", "Create order functionality coming soon", "OK");
    }
} 