using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zama.Data;
using Zama.Models;

namespace Zama.Services
{
    public interface IAuthService
    {
        Task<User> LoginAsync(string email, string password);
        Task<User> RegisterAsync(User user);
        Task LogoutAsync();
    }
}
namespace Zama.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ISecureStorage _secureStorage;
        private readonly ILogger<AuthService> _logger;
        private const string BaseUrl = "api/auth";

        public AuthService(HttpClient httpClient, ISecureStorage secureStorage, ILogger<AuthService> logger)
        {
            _httpClient = httpClient;
            _secureStorage = secureStorage;
            _logger = logger;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            try
            {
                var loginRequest = new { Email = email, Password = password };
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<User>();
                    await SaveUserData(user);
                    return user;
                }

                _logger.LogWarning("Failed login attempt for email {Email}", email);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email {Email}", email);
                throw;
            }
        }

        public async Task<User> RegisterAsync(User user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/register", user);
                response.EnsureSuccessStatusCode();

                var registeredUser = await response.Content.ReadFromJsonAsync<User>();
                await SaveUserData(registeredUser);
                return registeredUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for email {Email}", user.Email);
                throw;
            }
        }

        private async Task SaveUserData(User user)
        {
            if (user != null)
            {
                await _secureStorage.SetAsync("userId", user.Id.ToString());
                await _secureStorage.SetAsync("userEmail", user.Email);
                Preferences.Set("UserId", user.Id);
                Preferences.Set("IsLoggedIn", true);
                _logger.LogInformation("User data saved successfully for {Email}", user.Email);
            }
        }

        public Task LogoutAsync()
        {
            try
            {
                _secureStorage.Remove("userId");
                _secureStorage.Remove("userEmail");
                Preferences.Remove("UserId");
                Preferences.Remove("IsLoggedIn");
                _logger.LogInformation("User logged out successfully");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                throw;
            }
        }
    }
}