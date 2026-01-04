using System.Net.Mail;
using NotificationService.API.Settings;

namespace NotificationService.Domain.Services;

public interface IEmailSender
{
    Task Send(MailMessage message, EmailSettings settings);
}