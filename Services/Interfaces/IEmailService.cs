using NotificationService.Entities;

namespace NotificationService.Services.Interfaces;

public interface IEmailService
{
    Task Send(EmailPayload payload);
}