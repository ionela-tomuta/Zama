using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Zama.Models;

public interface IMenuService
{
    Task OpenMenuPdf();
    List<string> GetMenuCategories();
    List<string> GetDailySpecials();
}

public class MenuService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MenuService> _logger;
    private const string BaseUrl = "api/menu";

    public MenuService(HttpClient httpClient, ILogger<MenuService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<Zama.Models.MenuItem>> GetMenuItemsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/items");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Zama.Models.MenuItem>>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting menu items");
            throw;
        }
    }

    public async Task<List<string>> GetMenuCategoriesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/categories");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<string>>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting menu categories");
            throw;
        }
    }

    public async Task<List<Zama.Models.MenuItem>> GetDailySpecialsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/daily-specials");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Zama.Models.MenuItem>>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting daily specials");
            throw;
        }
    }

    // NOI METODE CRUD
    public async Task<Zama.Models.MenuItem> CreateMenuItemAsync(Zama.Models.MenuItem menuItem)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/items", menuItem);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Zama.Models.MenuItem>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating menu item");
            throw;
        }
    }

    public async Task<Zama.Models.MenuItem> GetMenuItemByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/items/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Zama.Models.MenuItem>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting menu item with ID {id}");
            throw;
        }
    }

    public async Task<Zama.Models.MenuItem> UpdateMenuItemAsync(int id, Zama.Models.MenuItem menuItem)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/items/{id}", menuItem);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Zama.Models.MenuItem>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating menu item with ID {id}");
            throw;
        }
    }

    public async Task<bool> DeleteMenuItemAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/items/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting menu item with ID {id}");
            throw;
        }
    }
}