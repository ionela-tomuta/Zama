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
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly DatabaseService _database;

        [ObservableProperty] private string name;
        [ObservableProperty] private string email;
        [ObservableProperty] private string password;
        [ObservableProperty] private string phoneNumber;
        [ObservableProperty] private DateTime dateOfBirth = DateTime.Now;

        public RegisterViewModel(DatabaseService database)
        {
            _database = database;
        }

        [RelayCommand]
        private async Task Register()
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phoneNumber))
            {
                await Shell.Current.DisplayAlert("Error", "Please fill all fields", "OK");
                return;
            }

            var user = new User
            {
                Name = name,
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber,
                DateOfBirth = dateOfBirth,
                Role = "Customer",
                RegistrationDate = DateTime.Now,
                LoyaltyPoints = 0
            };

            await _database.SaveUserAsync(user);
            await Shell.Current.GoToAsync("///login");
            await Shell.Current.DisplayAlert("Success", "Account created successfully!", "OK");
        }
    }
}
