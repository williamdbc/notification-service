using MediatR;

namespace NotificationService.Application.UseCases.CancelNotification;

public class CancelNotificationCommand : IRequest<Unit>
{
    public Guid NotificationId { get; }

    public CancelNotificationCommand(Guid notificationId)
    {
        NotificationId = notificationId;
    }
}