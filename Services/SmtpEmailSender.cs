using System.Net;
using System.Net.Mail;
using NotificationService.Settings;

namespace NotificationService.Services;

public class SmtpEmailSender : IEmailSender
{
    public async Task SendAsync(MailMessage message, EmailSettings settings)
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