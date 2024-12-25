using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Zama.Views;
using Zama.Models;
using Zama.Services;
using Microsoft.Maui.Storage;

=======
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1

namespace Zama.ViewModels
{
    public partial class ProfilePageViewModel : ObservableObject
    {
        private readonly DatabaseService _database;

        [ObservableProperty]
        private User currentUser;

        [ObservableProperty]
        private ObservableCollection<Reservation> userReservations;

        public ProfilePageViewModel(DatabaseService database)
        {
            _database = database;
            LoadUserData();
        }

        private async void LoadUserData()
        {
            var userId = Preferences.Get("UserId", 0);
            if (userId != 0)
            {
                currentUser = await _database.GetUserByIdAsync(userId);
                var reservations = await _database.GetUserReservationsAsync(userId);
                userReservations = new ObservableCollection<Reservation>(reservations);
            }
        }
    }
}
