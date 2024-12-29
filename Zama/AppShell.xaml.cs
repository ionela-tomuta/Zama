using Zama.Views;

namespace Zama
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Nu este nevoie să înregistrezi rutele pentru ShellContent
            // Înregistrează doar rutele pentru navigare "modală" sau particulară
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("register", typeof(RegisterPage));

            // Pentru rutele din TabBar, folosește calea completă
            Routing.RegisterRoute("main/home", typeof(MainPage));
            Routing.RegisterRoute("main/menu", typeof(MenuPage));
            Routing.RegisterRoute("main/reservations", typeof(ReservationsPage));
            Routing.RegisterRoute("main/profile", typeof(ProfilePage));
        }
    }
}