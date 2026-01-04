using Hangfire;
using NotificationService.Application.Abstractions.Dispatching;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Jobs;

namespace NotificationService.Infrastructure.Dispatching;

public class HangfireNotificationEnqueuer : INotificationEnqueuer
{
    public Task EnqueueAsync(Notification notification)
    {
        var queue = NotificationQueueResolver.Resolve(notification.Channel);

        if (notification.ScheduledAt.HasValue)
        {
            BackgroundJob.Schedule<NotificationJob>(
                queue,
                job => job.Send(notification.Id, null),
                notification.ScheduledAt.Value);
        }
        else
        {
            BackgroundJob.Enqueue<NotificationJob>(
                queue,
                job => job.Send(notification.Id, null));
        }

        return Task.CompletedTask;
    }
}