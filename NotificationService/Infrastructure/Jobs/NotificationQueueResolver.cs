using NotificationService.Domain.Enums;

namespace NotificationService.Infrastructure.Jobs;

public static class NotificationQueueResolver
{
    public static string Resolve(NotificationChannel channel)
    {
        return channel switch
        {
            NotificationChannel.Email => "email",
            NotificationChannel.Sms => "sms",
            NotificationChannel.Whatsapp => "whatsapp",
            _ => "default"
        };
    }
}