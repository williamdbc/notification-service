using MediatR;
using NotificationService.Domain.Enums;
using NotificationService.Domain.Exceptions;
using NotificationService.Domain.Repositories;

namespace NotificationService.Application.UseCases.CancelNotification;

public class CancelNotificationHandler : IRequestHandler<CancelNotificationCommand, Unit>
{
    private readonly INotificationRepository _notificationRepository;

    public CancelNotificationHandler(
        INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<Unit> Handle(
        CancelNotificationCommand request,
        CancellationToken cancellationToken)
    {
        var notification = await _notificationRepository.GetById(request.NotificationId);

        if (notification == null)
            throw new NotFoundException("Notification not found");

        if (notification.Status == NotificationStatus.Sent)
            throw new BusinessException("Notification already sent and cannot be canceled");

        notification.Cancel();

        await _notificationRepository.Update(notification);

        return Unit.Value;
    }
}