using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using DoctorDashboardApp.Services;
using DoctorDashboardApp.Views;
using DoctorDashboardApp.ViewModels;
using Microsoft.Maui.Platform;

namespace DoctorDashboardApp;

public partial class App : Application
{
    private readonly IDatabase _databaseService;
    private readonly IAuthService _authService;
    private readonly IServiceProvider _serviceProvider;

    public App(IDatabase databaseService, IAuthService authService, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _databaseService = databaseService;
        _authService = authService;
        _serviceProvider = serviceProvider;

        var loginViewModel = _serviceProvider.GetRequiredService<LoginViewModel>();
        MainPage = new AppShell();
        Routing.RegisterRoute("Login", typeof(LoginPage));
        Shell.Current.GoToAsync("//Login");
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);
        
        if (window != null)
        {
            // Configure the window
            window.Title = "Doctor Dashboard";
            window.Width = 1024;
            window.Height = 768;
            window.X = 100;
            window.Y = 100;

            // Ensure the window is visible
            window.Created += (s, e) =>
            {
                if (window.Handler?.PlatformView is UIKit.UIWindow uiWindow)
                {
                    uiWindow.MakeKeyAndVisible();
                }
            };

            // Handle window resume
            window.Resumed += (s, e) =>
            {
                if (window.Handler?.PlatformView is UIKit.UIWindow uiWindow)
                {
                    uiWindow.MakeKeyAndVisible();
                }
            };
        }

        return window;
    }

    protected override async void OnStart()
    {
        base.OnStart();
        await _databaseService.InitializeDatabaseAsync();
    }
}