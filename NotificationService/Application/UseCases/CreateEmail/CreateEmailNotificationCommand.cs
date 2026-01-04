using MediatR;

namespace NotificationService.Application.UseCases.CreateEmail;

public class CreateEmailNotificationCommand : IRequest<Guid>
{
    public string FromKey { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<string> To { get; set; } = new();
    public List<string>? Cc { get; set; }
    public List<string>? Bcc { get; set; }
    public DateTime? ScheduledAt { get; set; }
    public List<CreateEmailAttachmentRequest>? Attachments { get; set; }
}

public class CreateEmailAttachmentRequest
{
    public string Name { get; set; }
    public string ContentType { get; set; }
    public string Base64 { get; set; }
}