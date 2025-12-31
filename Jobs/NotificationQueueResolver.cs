using NotificationService.Enums;

namespace NotificationService.Jobs;

public static class NotificationQueueResolver
{
    public static string Resolve(NotificationChannel channel)
    {
        return channel switch
        {
            NotificationChannel.Email => "email",
            NotificationChannel.Sms => "sms",
            NotificationChannel.WhatsApp => "whatsapp",
            _ => "default"
        };
    }
}