<<<<<<< HEAD
﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Zama.Views;

namespace Zama.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IConnectivity _connectivity;

=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zama.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;

    public partial class MainPageViewModel : ObservableObject
    {
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1
        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string specialOfTheDay;

        [ObservableProperty]
        private decimal specialPrice;

<<<<<<< HEAD
        public MainPageViewModel(IConnectivity connectivity)
        {
            _connectivity = connectivity;
=======
        public MainPageViewModel()
        {
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1
            userName = "Guest User";
            specialOfTheDay = "Pizza Margherita";
            specialPrice = 9.99m;
        }

        [RelayCommand]
        private async Task MakeReservation()
        {
<<<<<<< HEAD
            if (!_connectivity.NetworkAccess.Equals(NetworkAccess.Internet))
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
=======
            await Shell.Current.GoToAsync("//ReservationsPage");
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1
        }

        [RelayCommand]
        private async Task ViewMenu()
        {
<<<<<<< HEAD
            if (!_connectivity.NetworkAccess.Equals(NetworkAccess.Internet))
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
=======
            await Shell.Current.GoToAsync("//MenuPage");
        }
    }
}
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1
