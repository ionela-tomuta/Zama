using Zama.ViewModels;

namespace Zama.Views
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage(MenuPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}