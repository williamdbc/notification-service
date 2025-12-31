using NotificationService.DTOs.Requests;
using NotificationService.Entities;

namespace NotificationService.Mappers;

public static class SmsPayloadMapper
{
    public static SmsPayload ToEntity(this CreateSmsNotificationRequest request)
    {
        return new SmsPayload
        {
            PhoneNumber = request.PhoneNumber,
            Message = request.Message
        };
    }
}