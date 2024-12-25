using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Zama
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            CreateNotificationChannel();
        }

        private void CreateNotificationChannel()
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(26))
            {
                var channelName = "Default";
                var channelDescription = "Default channel for app notifications";
                var channel = new NotificationChannel("default", channelName, NotificationImportance.Default)
                {
                    Description = channelDescription
                };

                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }
        }
    }
}