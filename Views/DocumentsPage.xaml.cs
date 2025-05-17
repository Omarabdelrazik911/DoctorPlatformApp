using DoctorDashboardApp.ViewModels;

namespace DoctorDashboardApp.Views;

public partial class DocumentsPage : ContentPage
{
    public DocumentsPage(DocumentsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
} 