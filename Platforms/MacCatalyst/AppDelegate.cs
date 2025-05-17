using Foundation;
using UIKit;
using Microsoft.Maui;

namespace DoctorDashboardApp
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);

            // Get the current window scene
            var scenes = UIApplication.SharedApplication.ConnectedScenes;
            var windowScene = scenes.ToArray().FirstOrDefault() as UIWindowScene;
            
            if (windowScene != null)
            {
                foreach (var window in windowScene.Windows)
                {
                    window.MakeKeyAndVisible();
                }
            }

            return result;
        }

        public override void OnActivated(UIApplication application)
        {
            base.OnActivated(application);

            // Get the current window scene
            var scenes = UIApplication.SharedApplication.ConnectedScenes;
            var windowScene = scenes.ToArray().FirstOrDefault() as UIWindowScene;
            
            if (windowScene != null)
            {
                foreach (var window in windowScene.Windows)
                {
                    window.MakeKeyAndVisible();
                }
            }
        }
    }
}