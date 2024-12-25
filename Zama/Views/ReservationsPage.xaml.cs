using Zama.ViewModels;

namespace Zama.Views
{
    public partial class ReservationsPage : ContentPage
    {
        public ReservationsPage()
        {
            InitializeComponent();
            BindingContext = new ReservationsPageViewModel();
        }
    }
}
