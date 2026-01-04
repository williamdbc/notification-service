using Hangfire;
using MediatR;
using NotificationService.Application.Abstractions.Dispatching;
using NotificationService.Application.Mappers;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Enums;
using NotificationService.Domain.Repositories;

namespace NotificationService.Application.UseCases.CreateEmail;

public class CreateEmailNotificationHandler : IRequestHandler<CreateEmailNotificationCommand, Guid>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationEnqueuer _notificationEnqueuer;

    public CreateEmailNotificationHandler(
        INotificationRepository notificationRepository,
        INotificationEnqueuer notificationEnqueuer)
    {
        _notificationRepository = notificationRepository;
        _notificationEnqueuer = notificationEnqueuer;
    }

    public async Task<Guid> Handle(
        CreateEmailNotificationCommand request,
        CancellationToken cancellationToken)
    {
        var emailPayload = request.ToEntity();

        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            Channel = NotificationChannel.Email,
            ScheduledAt = request.ScheduledAt,
            EmailPayload = emailPayload
        };

        notification.EnsureScheduledDateIsFuture();

        await _notificationRepository.Add(notification);

        await _notificationEnqueuer.EnqueueAsync(notification);
        
        return notification.Id;
    }
    
}