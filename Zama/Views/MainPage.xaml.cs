using Zama.ViewModels;

namespace Zama.Views
{
    public partial class MainPage : ContentPage
    {
<<<<<<< HEAD
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
=======
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1
        }
    }
}