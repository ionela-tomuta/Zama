using Microsoft.Maui.Platform;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zama.Models;

namespace Zama.Services
{
    public class NotificationService : IDataService<Notification>
    {
        private readonly DatabaseService _databaseService;
        private readonly INotificationManager _notificationManager;

        public NotificationService(DatabaseService databaseService, INotificationManager notificationManager)
        {
            _databaseService = databaseService;
            _notificationManager = notificationManager;
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            var db = _databaseService.GetConnection();
            var notifications = await db.Table<Notification>()
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();

            // Load related User data
            foreach (var notification in notifications)
            {
                notification.User = await db.Table<User>()
                    .Where(u => u.Id == notification.UserId)
                    .FirstOrDefaultAsync();
            }

            return notifications;
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            var db = _databaseService.GetConnection();
            var notification = await db.Table<Notification>()
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();

            if (notification != null)
            {
                notification.User = await db.Table<User>()
                    .Where(u => u.Id == notification.UserId)
                    .FirstOrDefaultAsync();
            }

            return notification;
        }

        public async Task<int> AddAsync(Notification notification)
        {
            var db = _databaseService.GetConnection();
            return await db.InsertAsync(notification);
        }

        public async Task<bool> UpdateAsync(Notification notification)
        {
            var db = _databaseService.GetConnection();
            var result = await db.UpdateAsync(notification);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var db = _databaseService.GetConnection();
            var result = await db.DeleteAsync<Notification>(id);
            return result > 0;
        }

        public async Task SendReservationConfirmation(Reservation reservation)
        {
            var notification = new Notification
            {
                UserId = reservation.UserId,
                Title = "Reservation Confirmed!",
                Message = $"Your table is booked for {reservation.ReservationDate:g}",
                CreatedDate = DateTime.Now,
                Type = "Reservation",
                IsRead = false
            };

            var db = _databaseService.GetConnection();
            await db.InsertAsync(notification);

            // Schedule local notification for 1 day before
            var scheduledTime = reservation.ReservationDate.AddDays(-1);
            if (scheduledTime > DateTime.Now)
            {
                await _notificationManager.ScheduleNotificationAsync(
                    notification.Id,
                    notification.Title,
                    notification.Message,
                    scheduledTime);
            }
        }

        public async Task SendOrderStatusUpdate(Order order)
        {
            var notification = new Notification
            {
                UserId = order.UserId,
                Title = "Order Status Update",
                Message = $"Your order #{order.Id} status: {order.Status}",
                CreatedDate = DateTime.Now,
                Type = "Order",
                IsRead = false
            };

            var db = _databaseService.GetConnection();
            await db.InsertAsync(notification);
            await _notificationManager.ShowNotificationAsync(notification.Title, notification.Message);
        }

        public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
        {
            var db = _databaseService.GetConnection();
            return await db.Table<Notification>()
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await GetByIdAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await UpdateAsync(notification);
            }
        }

        public async Task SendPromotionalNotification(int userId, string title, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                CreatedDate = DateTime.Now,
                Type = "Promotion",
                IsRead = false
            };

            var db = _databaseService.GetConnection();
            await db.InsertAsync(notification);
            await _notificationManager.ShowNotificationAsync(title, message);
        }
    }
}
