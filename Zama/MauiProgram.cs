using Microsoft.Extensions.Logging;
using Zama.Services;
using Zama.ViewModels;
using Zama.Views;

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

            ConfigureServices(builder.Services);
            ConfigureLogging(builder);

            return builder.Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Configurare HttpClient pentru servicii
            services.AddHttpClient("API", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7189/"); // Ajustează portul după nevoie
            });

            // Înregistrare HttpClient pentru fiecare serviciu
            services.AddHttpClient<AuthService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7189/");
            });

            services.AddHttpClient<ReservationService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7189/");
            });

            services.AddHttpClient<MenuService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7189/");
            });

            // Pages
            services.AddTransient<LoginPage>();
            services.AddTransient<RegisterPage>();
            services.AddTransient<MainPage>();
            services.AddTransient<MenuPage>();
            services.AddTransient<ReservationsPage>();
            services.AddTransient<ProfilePage>();
            services.AddTransient<AppShell>();

            // ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<MenuPageViewModel>();
            services.AddTransient<ReservationsPageViewModel>();
            services.AddTransient<ProfilePageViewModel>();

            // Servicii de infrastructură
            services.AddSingleton<IConnectivity>(Connectivity.Current);
            services.AddSingleton<ISecureStorage>(SecureStorage.Default);

            // Servicii de business
            services.AddScoped<AuthService>();
            services.AddScoped<ReservationService>();
            services.AddScoped<MenuService>();
        }

        private static void ConfigureLogging(MauiAppBuilder builder)
        {
            builder.Logging
                .AddDebug()
                .SetMinimumLevel(LogLevel.Debug);

#if DEBUG
            builder.Logging.AddConsole();
#endif
        }
    }
}