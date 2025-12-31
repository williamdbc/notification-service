using System.Net.Mail;
using NotificationService.Settings;

namespace NotificationService.Services;

public interface IEmailSender
{
    Task SendAsync(MailMessage message, EmailSettings settings);
}