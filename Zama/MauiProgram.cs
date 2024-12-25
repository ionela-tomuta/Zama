using Microsoft.Extensions.Logging;
using Zama.Views;
using Zama.ViewModels;
using Zama.Services;

namespace Zama
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

            // Înregistrarea serviciilor MAUI
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

            // Înregistrarea paginilor
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MenuPage>();
            builder.Services.AddTransient<ReservationsPage>();
            builder.Services.AddTransient<ProfilePage>();

            // Înregistrarea ViewModels
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MenuPageViewModel>();
            builder.Services.AddTransient<ReservationsPageViewModel>();
            builder.Services.AddTransient<ProfilePageViewModel>();

            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<AuthService>();
#if ANDROID
            builder.Services.AddSingleton<INotificationManager, NotificationManagerImplementation>();
#else
builder.Services.AddSingleton<INotificationManager, DummyNotificationManager>();
#endif
            builder.Services.AddSingleton<NotificationService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}