using System;
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
        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string specialOfTheDay;

        [ObservableProperty]
        private decimal specialPrice;

        public MainPageViewModel()
        {
            userName = "Guest User";
            specialOfTheDay = "Pizza Margherita";
            specialPrice = 9.99m;
        }

        [RelayCommand]
        private async Task MakeReservation()
        {
            await Shell.Current.GoToAsync("//ReservationsPage");
        }

        [RelayCommand]
        private async Task ViewMenu()
        {
            await Shell.Current.GoToAsync("//MenuPage");
        }
    }
}
