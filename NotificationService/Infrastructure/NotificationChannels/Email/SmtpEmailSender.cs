using System.Net;
using System.Net.Mail;
using NotificationService.API.Settings;
using NotificationService.Domain.Services;

namespace NotificationService.Infrastructure.NotificationChannels.Email;

public class SmtpEmailSender : IEmailSender
{
    public async Task Send(MailMessage message, EmailSettings settings)
    {
        using var smtpClient = new SmtpClient(settings.SmtpServer)
        {
            Port = int.Parse(settings.Port),
            Credentials = new NetworkCredential(settings.Username, settings.Password),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(message);
    }
}