using Microsoft.Maui.Controls;
using DoctorDashboardApp.ViewModels;

namespace DoctorDashboardApp.Views
{
    public partial class PatientDetailsPage : ContentPage
    {
        public PatientDetailsPage(PatientDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
} 