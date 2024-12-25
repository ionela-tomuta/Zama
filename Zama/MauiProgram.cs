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

<<<<<<< HEAD
            // Servicii de sistem
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

            // Servicii aplicație
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<NotificationService>();

            // Configurare notificări specifice platformei
#if ANDROID
            builder.Services.AddSingleton<INotificationManager, NotificationManagerImplementation>();
#else
                builder.Services.AddSingleton<INotificationManager, DummyNotificationManager>();
#endif

            // Înregistrare Pages și ViewModels
            RegisterPagesAndViewModels(builder.Services);
=======
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
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1

#if DEBUG
            builder.Logging.AddDebug();
#endif
<<<<<<< HEAD

            return builder.Build();
        }

        private static void RegisterPagesAndViewModels(IServiceCollection services)
        {
            // Pages
            services.AddTransient<MainPage>();
            services.AddTransient<MenuPage>();
            services.AddTransient<ReservationsPage>();
            services.AddTransient<ProfilePage>();

            // ViewModels
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<MenuPageViewModel>();
            services.AddTransient<ReservationsPageViewModel>();
            services.AddTransient<ProfilePageViewModel>();
        }
=======
            return builder.Build();
        }
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1
    }
}