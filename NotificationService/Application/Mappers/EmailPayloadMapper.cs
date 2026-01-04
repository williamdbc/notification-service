using NotificationService.Application.DTOs.Responses;
using NotificationService.Application.UseCases.CreateEmail;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Enums;

namespace NotificationService.Application.Mappers;

public static class EmailPayloadMapper
{
    public static EmailPayload ToEntity(this CreateEmailNotificationCommand request)
    {
        var payload = new EmailPayload
        {
            FromKey = request.FromKey,
            Subject = request.Subject,
            Body = request.Body
        };

        if (request.To != null)
            foreach (var email in request.To)
                payload.Recipients.Add(new EmailRecipient { Email = email, Type = RecipientType.To });

        if (request.Cc != null)
            foreach (var email in request.Cc)
                payload.Recipients.Add(new EmailRecipient { Email = email, Type = RecipientType.Cc });

        if (request.Bcc != null)
            foreach (var email in request.Bcc)
                payload.Recipients.Add(new EmailRecipient { Email = email, Type = RecipientType.Bcc });

        return payload;
    }

    public static EmailAttachment ToEntity(this CreateEmailAttachmentRequest request)
    {
        return new EmailAttachment
        {
            Name = request.Name,
            ContentType = request.ContentType,
            Base64 = request.Base64
        };
    }
    
    public static NotificationEmailResponse ToResponse(this Notification notification)
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
}