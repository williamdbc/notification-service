namespace NotificationService.Domain.Dispatching;

public interface INotificationSender
{
    public Task Dispatch(Guid notificationId);
}