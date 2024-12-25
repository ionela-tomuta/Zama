using Zama.Views;

namespace Zama
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
            Routing.RegisterRoute(nameof(ReservationsPage), typeof(ReservationsPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        }
    }
}