using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zama.Models;

namespace Zama.Services
{
    public class DummyNotificationManager : INotificationManager
    {
        public Task ScheduleNotificationAsync(int notificationId, string title, string message, DateTime scheduledTime)
        {
            return Task.CompletedTask;
        }

        public Task CancelNotificationAsync(int notificationId)
        {
            return Task.CompletedTask;
        }

        public Task ShowNotificationAsync(string title, string message)
        {
            return Task.CompletedTask;
        }
    }
}
