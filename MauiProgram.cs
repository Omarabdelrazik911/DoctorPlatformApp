using Microsoft.Extensions.Logging;
using DoctorDashboardApp.Services;
using DoctorDashboardApp.Views;
using DoctorDashboardApp.ViewModels;

namespace DoctorDashboardApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register Services
            builder.Services.AddSingleton<IDatabase, DatabaseService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<ISecureStorage>(SecureStorage.Default);

            // Register Pages
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<PatientsPage>();
            builder.Services.AddTransient<AppointmentsPage>();
            builder.Services.AddTransient<ReportsPage>();

            // Register ViewModels
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<PatientsViewModel>();
            builder.Services.AddTransient<PatientDetailsViewModel>();
            builder.Services.AddTransient<AppointmentsViewModel>();
            builder.Services.AddTransient<OrdersViewModel>();
            builder.Services.AddTransient<TestResultsViewModel>();
            builder.Services.AddTransient<DocumentsViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Initialize Database
            var dbService = app.Services.GetService<IDatabase>();
            dbService?.InitializeDatabaseAsync().Wait();

            return app;
        }
    }
}