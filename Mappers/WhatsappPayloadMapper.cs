using NotificationService.DTOs.Requests;
using NotificationService.Entities;

namespace NotificationService.Mappers;

public static class WhatsappPayloadMapper
{
    public static WhatsappPayload ToEntity(this CreateWhatsappNotificationRequest request)
    {
        return new WhatsappPayload
        {
            PhoneNumber = request.PhoneNumber,
            Message = request.Message
        };
    }
}