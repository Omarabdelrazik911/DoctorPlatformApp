using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoctorDashboardApp.Models;
using DoctorDashboardApp.Services;

namespace DoctorDashboardApp.ViewModels;

public partial class TestResultsViewModel : ObservableObject
{
    private readonly IDatabase _databaseService;
    
    [ObservableProperty]
    private ObservableCollection<TestResult> testResults;
    
    [ObservableProperty]
    private bool isLoading;
    
    [ObservableProperty]
    private string searchQuery;

    public TestResultsViewModel(IDatabase databaseService)
    {
        _databaseService = databaseService;
        TestResults = new ObservableCollection<TestResult>();
        LoadTestResults();
    }

    [RelayCommand]
    private async Task LoadTestResults()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            // TODO: Implement test results loading from database
            // For now, add sample data
            TestResults.Clear();
            TestResults.Add(new TestResult 
            { 
                PatientName = "John Doe",
                TestName = "Blood Glucose",
                Result = "120 mg/dL",
                Status = "Normal",
                TestDate = DateTime.Now.AddDays(-1)
            });
            TestResults.Add(new TestResult 
            { 
                PatientName = "Jane Smith",
                TestName = "Complete Blood Count",
                Result = "See Details",
                Status = "Abnormal",
                TestDate = DateTime.Now.AddDays(-2)
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
    private async Task SearchTestResults()
    {
        await LoadTestResults();
        if (string.IsNullOrWhiteSpace(SearchQuery)) return;
        
        var filteredResults = TestResults.Where(t => 
            t.PatientName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
            t.TestName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
            t.Result.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();
            
        TestResults.Clear();
        foreach (var result in filteredResults)
        {
            TestResults.Add(result);
        }
    }

    [RelayCommand]
    private async Task ViewTestDetails(TestResult result)
    {
        // TODO: Implement test details view
        await Shell.Current.DisplayAlert("Test Details", 
            $"Patient: {result.PatientName}\nTest: {result.TestName}\nResult: {result.Result}\nDate: {result.TestDate:d}", 
            "OK");
    }
} 