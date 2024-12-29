using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Zama.Data;
using Zama.Models;

public interface INotificationManager
{
    Task ScheduleNotificationAsync(int notificationId, string title, string message, DateTime scheduledTime);
    Task CancelNotificationAsync(int notificationId);
    Task ShowNotificationAsync(string title, string message);
}


namespace Zama.Services
{
    public interface INotificationService
    {
        Task<List<Notification>> GetAllAsync();
        Task<Notification> GetByIdAsync(int id);
        Task<int> AddAsync(Notification notification);
        Task<bool> UpdateAsync(Notification notification);
        Task<bool> DeleteAsync(int id);
        Task SendReservationConfirmation(Reservation reservation);
        Task<List<Notification>> GetUserNotificationsAsync(int userId);
        Task MarkAsReadAsync(int notificationId);
        Task SendPromotionalNotification(int userId, string title, string message);
    }

    public class NotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NotificationService> _logger;
        private readonly INotificationManager _notificationManager;
        private const string BaseUrl = "api/notifications";

        public NotificationService(
            HttpClient httpClient,
            ILogger<NotificationService> logger,
            INotificationManager notificationManager)
        {
            _httpClient = httpClient;
            _logger = logger;
            _notificationManager = notificationManager;
        }

        public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/user/{userId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<Notification>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user notifications");
                throw;
            }
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            try
            {
                var response = await _httpClient.PutAsync($"{BaseUrl}/{notificationId}/read", null);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking notification as read");
                throw;
            }
        }

        public async Task<bool> DeleteNotificationAsync(int notificationId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/{notificationId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting notification");
                throw;
            }
        }
    }
}