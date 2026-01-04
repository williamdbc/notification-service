using NotificationService.Domain.Entities;

namespace NotificationService.Application.Abstractions.Dispatching;

public interface INotificationEnqueuer
{
    Task EnqueueAsync(Notification notification);
}