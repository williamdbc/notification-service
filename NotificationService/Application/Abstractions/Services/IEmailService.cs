using NotificationService.Domain.Entities;

namespace NotificationService.Application.Abstractions.Services;

public interface IEmailService
{
    Task Send(EmailPayload payload);
}