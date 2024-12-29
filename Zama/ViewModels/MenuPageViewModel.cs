using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Zama.Services;
using Microsoft.Extensions.Logging;
using Zama.Models;

namespace Zama.ViewModels
{
    public partial class MenuPageViewModel : ObservableObject
    {
        private readonly MenuService _menuService;
        private readonly ILogger<MenuPageViewModel> _logger;

        [ObservableProperty]
        private ObservableCollection<Zama.Models.MenuItem> _menuItems = new();

        [ObservableProperty]
        private ObservableCollection<Zama.Models.MenuItem> _dailySpecials = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private Zama.Models.MenuItem _currentMenuItem;

        public MenuPageViewModel(MenuService menuService, ILogger<MenuPageViewModel> logger)
        {
            _menuService = menuService;
            _logger = logger;
            _currentMenuItem = new Zama.Models.MenuItem();
        }

        [RelayCommand]
        private async Task LoadMenuDataAsync()
        {
            if (_isBusy) return;
            try
            {
                _isBusy = true;
                var items = await _menuService.GetMenuItemsAsync();
                _menuItems.Clear();
                foreach (var item in items)
                {
                    _menuItems.Add(item);
                }
                var specials = await _menuService.GetDailySpecialsAsync();
                _dailySpecials.Clear();
                foreach (var special in specials)
                {
                    _dailySpecials.Add(special);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading menu data");
                await Shell.Current.DisplayAlert("Error", "Could not load menu data", "OK");
            }
            finally
            {
                _isBusy = false;
            }
        }

        // NOI METODE CRUD

        [RelayCommand]
        private async Task LoadAllMenuItemsAsync()
        {
            if (_isBusy) return;
            try
            {
                _isBusy = true;
                var items = await _menuService.GetMenuItemsAsync();
                _menuItems.Clear();
                foreach (var item in items)
                {
                    _menuItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading menu items");
                await Shell.Current.DisplayAlert("Eroare", "Nu s-au putut încărca itemii de meniu", "OK");
            }
            finally
            {
                _isBusy = false;
            }
        }

        [RelayCommand]
        private void SelectMenuItem(Zama.Models.MenuItem menuItem)
        {
            _currentMenuItem = new Zama.Models.MenuItem
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                Category = menuItem.Category,
                IsAvailable = menuItem.IsAvailable,
                Allergens = menuItem.Allergens,
                DietaryLabels = menuItem.DietaryLabels,
                ImageUrl = menuItem.ImageUrl,
                AverageRating = menuItem.AverageRating,
                TimesOrdered = menuItem.TimesOrdered,
                IsSpecialOfTheDay = menuItem.IsSpecialOfTheDay,
                SpecialPrice = menuItem.SpecialPrice,
                PrepTimeMinutes = menuItem.PrepTimeMinutes
            };
        }

        [RelayCommand]
        private async Task CreateMenuItemAsync()
        {
            if (_isBusy) return;
            try
            {
                _isBusy = true;
                var newMenuItem = await _menuService.CreateMenuItemAsync(_currentMenuItem);
                _menuItems.Add(newMenuItem);
                await Shell.Current.DisplayAlert("Succes", "Item de meniu creat", "OK");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating menu item");
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut crea item-ul de meniu", "OK");
            }
            finally
            {
                _isBusy = false;
            }
        }

        [RelayCommand]
        private async Task UpdateMenuItemAsync()
        {
            if (_isBusy) return;
            try
            {
                _isBusy = true;
                if (_currentMenuItem.Id == 0)
                {
                    await Shell.Current.DisplayAlert("Eroare", "Selectați un item de meniu pentru actualizare", "OK");
                    return;
                }

                var updatedMenuItem = await _menuService.UpdateMenuItemAsync(_currentMenuItem.Id, _currentMenuItem);

                var index = _menuItems.IndexOf(_menuItems.FirstOrDefault(m => m.Id == updatedMenuItem.Id));
                if (index != -1)
                {
                    _menuItems[index] = updatedMenuItem;
                }

                await Shell.Current.DisplayAlert("Succes", "Item de meniu actualizat", "OK");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating menu item");
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut actualiza item-ul de meniu", "OK");
            }
            finally
            {
                _isBusy = false;
            }
        }

        [RelayCommand]
        private async Task DeleteMenuItemAsync(int menuItemId)
        {
            if (_isBusy) return;
            try
            {
                _isBusy = true;
                var result = await _menuService.DeleteMenuItemAsync(menuItemId);

                if (result)
                {
                    var menuItemToRemove = _menuItems.FirstOrDefault(m => m.Id == menuItemId);
                    if (menuItemToRemove != null)
                    {
                        _menuItems.Remove(menuItemToRemove);
                    }
                    await Shell.Current.DisplayAlert("Succes", "Item de meniu șters", "OK");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting menu item");
                await Shell.Current.DisplayAlert("Eroare", "Nu s-a putut șterge item-ul de meniu", "OK");
            }
            finally
            {
                _isBusy = false;
            }
        }
    }
}