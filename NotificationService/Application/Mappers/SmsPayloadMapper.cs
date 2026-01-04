using NotificationService.Application.DTOs.Responses;
using NotificationService.Application.UseCases.CreateSms;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Mappers;

public static class SmsPayloadMapper
{
    public static SmsPayload ToEntity(this CreateSmsNotificationCommand request)
    {
        return new SmsPayload
        {
            PhoneNumber = request.PhoneNumber,
            Message = request.Message
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

}