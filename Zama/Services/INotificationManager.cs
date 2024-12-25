using Microsoft.Maui.Platform;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zama.Models;


namespace Zama.Services
{
    public interface INotificationManager
    {
        Task ScheduleNotificationAsync(int notificationId, string title, string message, DateTime scheduledTime);
        Task CancelNotificationAsync(int notificationId);
        Task ShowNotificationAsync(string title, string message);
    }
}
