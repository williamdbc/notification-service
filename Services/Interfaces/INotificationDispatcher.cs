namespace NotificationService.Services.Interfaces;

public interface INotificationDispatcher
{
    public Task Dispatch(Guid notificationId);
}