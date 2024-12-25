using Zama.Views;

namespace Zama
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
<<<<<<< HEAD
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("register", typeof(RegisterPage));
=======
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
            Routing.RegisterRoute(nameof(ReservationsPage), typeof(ReservationsPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1
        }
    }
}