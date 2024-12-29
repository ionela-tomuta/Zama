using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Zama.Services;
using Zama.Models;

namespace Zama.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LoginViewModel> _logger;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [ObservableProperty]
        private bool _isBusy;

        public LoginViewModel(IAuthService authService, ILogger<LoginViewModel> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                if (!ValidateInput()) return;

                var user = await _authService.LoginAsync(Email, Password);

                if (user == null)
                {
                    ErrorMessage = "Email sau parolă incorectă";
                    await Shell.Current.DisplayAlert("Eroare", "Email sau parolă incorectă", "OK");
                    return;
                }

                // Autentificare reușită
                await Shell.Current.DisplayAlert("Succes", "Autentificare reușită!", "OK");
                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la autentificare");
                ErrorMessage = "A apărut o eroare la autentificare. Încercați din nou.";
                await Shell.Current.DisplayAlert("Eroare", "A apărut o eroare la autentificare. Încercați din nou.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Introduceți adresa de email";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Introduceți parola";
                return false;
            }

            if (!IsValidEmail(Email))
            {
                ErrorMessage = "Adresa de email nu este validă";
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
        private async Task GoToRegisterAsync()
        {
            if (IsBusy) return;
            await Shell.Current.GoToAsync("//RegisterPage");
        }
    }
}