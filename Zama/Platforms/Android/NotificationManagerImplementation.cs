using Android.App;
using Android.Content;
using AndroidX.Core.App;
using Application = Android.App.Application;

namespace Zama.Services
{
    public class NotificationManagerImplementation : INotificationManager
    {
        public async Task ScheduleNotificationAsync(int notificationId, string title, string message, DateTime scheduledTime)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                var context = Application.Context;
                var intent = new Intent(context, typeof(MainActivity));
                var pendingIntent = PendingIntent.GetActivity(
                    context,
                    notificationId,
                    intent,
                    PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

                var notificationBuilder = new NotificationCompat.Builder(context, "default")
                    .SetContentTitle(title)
                    .SetContentText(message)
                    .SetSmallIcon(Resource.Mipmap.appicon)
                    .SetAutoCancel(true)
                    .SetContentIntent(pendingIntent);

                var notificationManager = NotificationManagerCompat.From(context);
                var delay = scheduledTime - DateTime.Now;
                if (delay.TotalMilliseconds > 0)
                {
                    await Task.Delay(delay);
                    notificationManager.Notify(notificationId, notificationBuilder.Build());
                }
            }
        }

        public async Task CancelNotificationAsync(int notificationId)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                var context = Application.Context;
                var notificationManager = NotificationManagerCompat.From(context);
                notificationManager.Cancel(notificationId);
            }
            await Task.CompletedTask;
        }

        public async Task ShowNotificationAsync(string title, string message)
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                var context = Application.Context;
                var notificationManager = NotificationManagerCompat.From(context);
                var intent = new Intent(context, typeof(MainActivity));
                var pendingIntent = PendingIntent.GetActivity(
                    context,
                    0,
                    intent,
                    PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

                var notificationBuilder = new NotificationCompat.Builder(context, "default")
                    .SetContentTitle(title)
                    .SetContentText(message)
                    .SetSmallIcon(Resource.Mipmap.appicon)
                    .SetAutoCancel(true)
                    .SetContentIntent(pendingIntent);

                notificationManager.Notify(DateTime.Now.Millisecond, notificationBuilder.Build());
            }
            await Task.CompletedTask;
        }
    }
}