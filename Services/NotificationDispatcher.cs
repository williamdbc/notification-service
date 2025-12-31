using NotificationService.Enums;
using NotificationService.Repositories;
using NotificationService.Services.Interfaces;

namespace NotificationService.Services;

public class NotificationDispatcher : INotificationDispatcher
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IEmailService _emailService;
    private readonly ISmsService _smsService;
    private readonly IWhatsAppService _whatsAppService;

    public NotificationDispatcher(
        INotificationRepository notificationRepository,
        IEmailService emailService,
        ISmsService smsService,
        IWhatsAppService whatsAppService)
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

            case NotificationChannel.WhatsApp:
                // await _whatsappService.Send(notification.WhatsappPayload);
                break;
        }

        notification.MarkAsSent();
        await _notificationRepository.Update(notification);
    }
}