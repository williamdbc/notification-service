using NotificationService.Application.UseCases.CreateWhatsapp;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Mappers;

public static class WhatsappPayloadMapper
{
    public static WhatsappPayload ToEntity(this CreateWhatsappNotificationCommand request)
    {
        return new WhatsappPayload
        {
            PhoneNumber = request.PhoneNumber,
            Message = request.Message
        };
    }
    
}