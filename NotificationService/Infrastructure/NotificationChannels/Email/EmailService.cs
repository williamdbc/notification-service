using System.Net.Mail;
using NotificationService.API.Settings;
using NotificationService.Application.Abstractions.Services;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Exceptions;
using NotificationService.Domain.Services;

namespace NotificationService.Infrastructure.NotificationChannels.Email;

public class EmailService : IEmailService
{
    private readonly EmailsSettings _emailsSettings;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        EmailsSettings emailsSettings,
        IEmailSender emailSender,
        ILogger<EmailService> logger)
    {
        _emailsSettings = emailsSettings;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task Send(EmailPayload payload)
    {
        try
        {
            var settings = ResolveEmailSettings(payload.FromKey);
            using var message = BuildMailMessage(payload, settings);
            
            await _emailSender.Send(message, settings);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar e-mail | FromKey: {FromKey}", payload.FromKey);
            throw new BusinessException("Não foi possível enviar o e-mail.");
        }
    }
    
    private EmailSettings ResolveEmailSettings(string fromKey)
    {
        var settings = _emailsSettings
            .FirstOrDefault(x => x.FromKey == fromKey);

        if (settings == null)
            throw new BusinessException($"Configuração de e-mail não encontrada para a chave '{fromKey}'.");

        return settings;
    }

    private MailMessage BuildMailMessage(
        EmailPayload payload,
        EmailSettings settings)
    {
        var message = new MailMessage
        {
            From = new MailAddress(settings.FromAddress, settings.DisplayName),
            Subject = payload.Subject,
            Body = payload.Body,
            IsBodyHtml = true
        };

        foreach (var to in payload.To)
            message.To.Add(to.Email);

        foreach (var cc in payload.Cc)
            message.CC.Add(cc.Email);

        foreach (var bcc in payload.Bcc)
            message.Bcc.Add(bcc.Email);

        foreach (var attachment in payload.Attachments)
        {
            var bytes = Convert.FromBase64String(attachment.Base64);
            var stream = new MemoryStream(bytes);

            message.Attachments.Add(
                new Attachment(stream, attachment.Name, attachment.ContentType));
        }

        return message;
    }
    
}