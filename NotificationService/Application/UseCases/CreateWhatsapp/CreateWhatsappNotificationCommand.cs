using MediatR;

namespace NotificationService.Application.UseCases.CreateWhatsapp;

public class CreateWhatsappNotificationCommand : IRequest<Guid>
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; } 
    public DateTime? ScheduledAt { get; set; }
}