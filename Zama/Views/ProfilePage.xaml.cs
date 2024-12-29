using Zama.ViewModels;

namespace Zama.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage(ProfilePageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is ProfilePageViewModel viewModel)
            {
                viewModel.LoadUserDataCommand.Execute(null);
            }
        }
    }
}