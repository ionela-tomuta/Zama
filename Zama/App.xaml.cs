using Microsoft.Extensions.Logging;
using Zama.Data;
using Zama.Views;

namespace Zama
{
    public partial class App : Application
    {
        private readonly ZamaDbContext _context;
        private readonly ILogger<App> _logger;
        private readonly IServiceProvider _serviceProvider;

        public App(ZamaDbContext context, ILogger<App> logger, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _context = context;
            _logger = logger;
            _serviceProvider = serviceProvider;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    var userId = Preferences.Get("UserId", 0);
                    var isLoggedIn = Preferences.Get("IsLoggedIn", false);

                    if (userId == 0 || !isLoggedIn)
                    {
                        MainPage = new AppShell();
                        Shell.Current.GoToAsync("//LoginPage");
                    }
                    else
                    {
                        MainPage = new AppShell();
                        Shell.Current.GoToAsync("//MainPage");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error initializing application");
                    MainPage = new AppShell();
                    Shell.Current.GoToAsync("//LoginPage");
                }
            });
        }
    }
}