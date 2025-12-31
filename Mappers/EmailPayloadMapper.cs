using NotificationService.DTOs.Requests;
using NotificationService.Entities;
using NotificationService.Enums;

namespace NotificationService.Mappers;

public static class EmailPayloadMapper
{
    public static EmailPayload ToEntity(this CreateEmailNotificationRequest request)
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
}