using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Zama.Data;
using Zama.Models;
using Zama.Services;

namespace Zama.ViewModels
{
    public partial class ProfilePageViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly ReservationService _reservationService;
        private readonly IProfileService _profileService;
        private readonly ILogger<ProfilePageViewModel> _logger;

        [ObservableProperty]
        private User _currentUser;

        [ObservableProperty]
        private ObservableCollection<Reservation> _userReservations;

        [ObservableProperty]
        private ObservableCollection<User> _allProfiles = new();

        [ObservableProperty]
        private bool _isBusy;

        public ProfilePageViewModel(
            AuthService authService,
            ReservationService reservationService,
            IProfileService profileService,
            ILogger<ProfilePageViewModel> logger)
        {
            _authService = authService;
            _reservationService = reservationService;
            _profileService = profileService;
            _logger = logger;
            UserReservations = new ObservableCollection<Reservation>();
        }

        [RelayCommand]
        public async Task LoadUserDataAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                var userId = Preferences.Get("UserId", 0);
                if (userId == 0)
                {
                    await Shell.Current.GoToAsync("//LoginPage");
                    return;
                }
                var reservations = await _reservationService.GetUserReservationsAsync(userId);
                UserReservations.Clear();
                foreach (var reservation in reservations)
                {
                    UserReservations.Add(reservation);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading user data");
                await Shell.Current.DisplayAlert("Error", "Could not load user data", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task LogoutAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                await _authService.LogoutAsync();
                await Shell.Current.GoToAsync("//LoginPage");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                await Shell.Current.DisplayAlert("Error", "Could not log out", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // CREATE
        [RelayCommand]
        private async Task CreateProfileAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                var newProfile = await _profileService.CreateProfileAsync(CurrentUser);
                _allProfiles.Add(newProfile);
                await Shell.Current.DisplayAlert("Succes", "Profil creat", "OK");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating profile");
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut crea profilul", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // READ (All Profiles)
        [RelayCommand]
        private async Task LoadAllProfilesAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                var profiles = await _profileService.GetAllProfilesAsync();
                _allProfiles.Clear();
                foreach (var profile in profiles)
                {
                    _allProfiles.Add(profile);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading profiles");
                await Shell.Current.DisplayAlert("Eroare", "Nu s-au putut încărca profilurile", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // UPDATE
        [RelayCommand]
        private async Task UpdateProfileAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                if (CurrentUser.Id == 0)
                {
                    await Shell.Current.DisplayAlert("Eroare", "Selectați un profil pentru actualizare", "OK");
                    return;
                }

                var updatedProfile = await _profileService.UpdateProfileAsync(CurrentUser.Id, CurrentUser);

                // Actualizare în lista de profiluri
                var index = _allProfiles.IndexOf(_allProfiles.FirstOrDefault(p => p.Id == updatedProfile.Id));
                if (index != -1)
                {
                    _allProfiles[index] = updatedProfile;
                }

                await Shell.Current.DisplayAlert("Succes", "Profil actualizat", "OK");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating profile");
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut actualiza profilul", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        // DELETE
        [RelayCommand]
        private async Task DeleteProfileAsync(int profileId)
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                var result = await _profileService.DeleteProfileAsync(profileId);

                if (result)
                {
                    var profileToRemove = _allProfiles.FirstOrDefault(p => p.Id == profileId);
                    if (profileToRemove != null)
                    {
                        _allProfiles.Remove(profileToRemove);
                    }
                    await Shell.Current.DisplayAlert("Succes", "Profil șters", "OK");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting profile");
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut șterge profilul", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}