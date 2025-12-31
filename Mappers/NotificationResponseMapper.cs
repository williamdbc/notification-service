using NotificationService.DTOs.Responses;
using NotificationService.Entities;
using NotificationService.Enums;

namespace NotificationService.Mappers;

public static class NotificationResponseMapper
{
    public static NotificationEmailResponse ToEmailResponse(this Notification notification)
    {
        var payload = notification.EmailPayload;

        return new NotificationEmailResponse
        {
            Id = notification.Id,
            Channel = notification.Channel.ToString(),
            Status = notification.Status.ToString(),
            CreatedAt = notification.CreatedAt,
            ScheduledAt = notification.ScheduledAt,
            SentAt = notification.SentAt,

            FromKey = payload.FromKey,
            Subject = payload.Subject,
            Body = payload.Body,

            To = payload.Recipients
                .Where(r => r.Type == RecipientType.To)
                .Select(r => new EmailRecipientResponse { Email = r.Email, Type = r.Type.ToString() })
                .ToList(),

            Cc = payload.Recipients
                .Where(r => r.Type == RecipientType.Cc)
                .Select(r => new EmailRecipientResponse { Email = r.Email, Type = r.Type.ToString() })
                .ToList(),

            Bcc = payload.Recipients
                .Where(r => r.Type == RecipientType.Bcc)
                .Select(r => new EmailRecipientResponse { Email = r.Email, Type = r.Type.ToString() })
                .ToList(),

            Attachments = payload.Attachments
                .Select(a => new EmailAttachmentResponse { Name = a.Name, ContentType = a.ContentType, Base64 = a.Base64 })
                .ToList()
        };
    }


    public static NotificationSmsResponse ToSmsResponse(this Notification notification)
    {
        return new NotificationSmsResponse
        {
            Id = notification.Id,
            Channel = notification.Channel.ToString(),
            Status = notification.Status.ToString(),
            CreatedAt = notification.CreatedAt,
            ScheduledAt = notification.ScheduledAt,
            SentAt = notification.SentAt,
            SmsPayload = notification.SmsPayload
        };
    }

    public static NotificationWhatsappResponse ToWhatsappResponse(this Notification notification)
    {
        return new NotificationWhatsappResponse
        {
            Id = notification.Id,
            Channel = notification.Channel.ToString(),
            Status = notification.Status.ToString(),
            CreatedAt = notification.CreatedAt,
            ScheduledAt = notification.ScheduledAt,
            SentAt = notification.SentAt,
            WhatsappPayload = notification.WhatsappPayload
        };
    }
}