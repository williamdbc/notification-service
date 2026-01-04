using System.Net.Mail;
using NotificationService.API.Settings;

namespace NotificationService.Application.Abstractions.Gateways;

public interface IEmailSender
{
    Task SendAsync(MailMessage message, EmailSettings settings);
}