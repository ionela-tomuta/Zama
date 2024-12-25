using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zama.Models;
using Zama.Services;
using Microsoft.Maui.Storage;

namespace Zama.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly DatabaseService _database;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        public LoginViewModel(DatabaseService database)
        {
            _database = database;
        }

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await Shell.Current.DisplayAlert("Error", "Please enter email and password", "OK");
                return;
            }

            var user = await _database.GetUserAsync(email, password);
            if (user != null)
            {
                // Store user session
                Preferences.Set("UserId", user.Id);
                await Shell.Current.GoToAsync("///main");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Invalid credentials", "OK");
            }
        }

        [RelayCommand]
        private async Task GoToRegister()
        {
            await Shell.Current.GoToAsync("register");
        }
    }
}
