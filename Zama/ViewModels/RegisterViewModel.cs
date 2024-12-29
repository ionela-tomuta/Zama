using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Zama.Models;
using Zama.Services;

namespace Zama.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly ILogger<RegisterViewModel> _logger;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private string _confirmPassword = string.Empty;

        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [ObservableProperty]
        private bool _isBusy;

        public RegisterViewModel(AuthService authService, ILogger<RegisterViewModel> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                _logger.LogInformation("Starting registration process");

                if (!ValidateInput()) return;

                var newUser = new User
                {
                    Name = Name,
                    Email = Email,
                    Password = Password, // API va face hash-ul parolei
                    PhoneNumber = PhoneNumber,
                    Role = "User",
                    LoyaltyPoints = 0,
                    RegistrationDate = DateTime.UtcNow
                };

                var registeredUser = await _authService.RegisterAsync(newUser);
                if (registeredUser != null)
                {
                    await Shell.Current.DisplayAlert("Succes", "Înregistrare reușită!", "OK");
                    ResetFields();
                    await NavigateToLogin();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration error");
                ErrorMessage = "A apărut o eroare la înregistrare";
                await Shell.Current.DisplayAlert("Eroare", ErrorMessage, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Introduceți numele";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Introduceți adresa de email";
                return false;
            }

            if (!IsValidEmail(Email))
            {
                ErrorMessage = "Adresa de email nu este validă";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Introduceți parola";
                return false;
            }

            if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = "Confirmați parola";
                return false;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Parolele nu coincid";
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        [RelayCommand]
        private async Task GoToLoginAsync()
        {
            if (IsBusy) return;
            await NavigateToLogin();
        }

        private void ResetFields()
        {
            Name = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            PhoneNumber = string.Empty;
            ErrorMessage = string.Empty;
        }

        private async Task NavigateToLogin()
        {
            try
            {
                await Shell.Current.GoToAsync("///login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Navigation error");
                ErrorMessage = "Eroare la navigare către pagina de login";
            }
        }
    }
}