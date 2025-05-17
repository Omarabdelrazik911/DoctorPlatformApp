using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoctorDashboardApp.Models;
using DoctorDashboardApp.Services;

namespace DoctorDashboardApp.ViewModels;

public partial class DocumentsViewModel : ObservableObject
{
    private readonly IDatabase _databaseService;
    
    [ObservableProperty]
    private ObservableCollection<Document> documents;
    
    [ObservableProperty]
    private bool isLoading;
    
    [ObservableProperty]
    private string searchQuery;

    public DocumentsViewModel(IDatabase databaseService)
    {
        _databaseService = databaseService;
        Documents = new ObservableCollection<Document>();
        LoadDocuments();
    }

    [RelayCommand]
    private async Task LoadDocuments()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            // TODO: Implement document loading from database
            // For now, add some sample data
            Documents.Clear();
            Documents.Add(new Document 
            { 
                Title = "Patient History Report",
                Date = DateTime.Now.AddDays(-1),
                Type = "Medical Report"
            });
            Documents.Add(new Document 
            { 
                Title = "Lab Results",
                Date = DateTime.Now.AddDays(-2),
                Type = "Test Results"
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
    private async Task SearchDocuments()
    {
        await LoadDocuments();
        if (string.IsNullOrWhiteSpace(SearchQuery)) return;
        
        var filteredDocs = Documents.Where(d => 
            d.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
            d.Type.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();
            
        Documents.Clear();
        foreach (var doc in filteredDocs)
        {
            Documents.Add(doc);
        }
    }
} 