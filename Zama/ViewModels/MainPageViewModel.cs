using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Zama.Views;

namespace Zama.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IConnectivity _connectivity;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string specialOfTheDay;

        [ObservableProperty]
        private decimal specialPrice;

        public MainPageViewModel(IConnectivity connectivity)
        {
            _connectivity = connectivity;
            userName = "Guest User";
            specialOfTheDay = "Pizza Margherita";
            specialPrice = 9.99m;
        }

        [RelayCommand]
        private async Task MakeReservation()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No Internet", "Please check your internet connection", "OK");
                return;
            }

            try
            {
                await Shell.Current.GoToAsync($"//{nameof(ReservationsPage)}");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task ViewMenu()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No Internet", "Please check your internet connection", "OK");
                return;
            }

            try
            {
                await Shell.Current.GoToAsync($"//{nameof(MenuPage)}");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
