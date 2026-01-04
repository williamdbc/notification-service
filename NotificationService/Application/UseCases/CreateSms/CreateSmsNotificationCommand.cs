using MediatR;

namespace NotificationService.Application.UseCases.CreateSms;

public class CreateSmsNotificationCommand : IRequest<Guid>
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public DateTime? ScheduledAt { get; set; }
}