using NotificationService.Application.Abstractions.Services;
using NotificationService.Domain.Dispatching;
using NotificationService.Domain.Enums;
using NotificationService.Domain.Repositories;

namespace NotificationService.Infrastructure.Dispatching;

public class NotificationSender : INotificationSender
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IEmailService _emailService;
    private readonly ISmsService _smsService;
    private readonly IWhatsappService _whatsAppService;

    public NotificationSender(
        INotificationRepository notificationRepository,
        IEmailService emailService,
        ISmsService smsService,
        IWhatsappService whatsAppService)
    {
        _notificationRepository = notificationRepository;
        _emailService = emailService;
        _smsService = smsService;
        _whatsAppService = whatsAppService;
    }

    public async Task Dispatch(Guid notificationId)
    {
        var notification = await _notificationRepository.GetFullNotification(notificationId);

        if (!notification.CanBeSent())
            return;

        switch (notification.Channel)
        {
            case NotificationChannel.Email:
                await _emailService.Send(notification.EmailPayload);
                break;

            case NotificationChannel.Sms:
                // await _smsService.Send(notification.SmsPayload);
                break;

            case NotificationChannel.Whatsapp:
                // await _whatsappService.Send(notification.WhatsappPayload);
                break;
        }

        notification.MarkAsSent();
        await _notificationRepository.Update(notification);
    }
}