using Microsoft.Maui.Controls;

namespace DoctorDashboardApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            resultLabel.Text = $"Saved record for {patientName.Text}";
        }
    }
}