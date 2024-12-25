using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zama.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using static Microsoft.Maui.ApplicationModel.Permissions;
using System.Xml.Linq;

namespace Zama.ViewModels
{
    public partial class ReservationsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string phone;

        [ObservableProperty]
        private DateTime reservationDate = DateTime.Now;

        [ObservableProperty]
        private TimeSpan reservationTime = new TimeSpan(19, 0, 0);

        [ObservableProperty]
        private int numberOfGuests = 2;

        [ObservableProperty]
        private string specialRequests;

        [RelayCommand]
        private async Task SubmitReservation()
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter your name", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter your phone number", "OK");
                return;
            }

            if (numberOfGuests < 1)
            {
                await Shell.Current.DisplayAlert("Error", "Please enter a valid number of guests", "OK");
                return;
            }

            await Shell.Current.DisplayAlert("Success", "Your reservation has been submitted successfully!", "OK");

            name = "";
            phone = "";
            reservationDate = DateTime.Now;
            reservationTime = new TimeSpan(19, 0, 0);
            numberOfGuests = 2;
            specialRequests = "";
        }
    }
}