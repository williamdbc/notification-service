using NotificationService.Entities;

namespace NotificationService.DTOs.Responses;

public class NotificationEmailResponse
{
    public Guid Id { get; set; }
    public string Channel { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ScheduledAt { get; set; }
    public DateTimeOffset? SentAt { get; set; }

    public string FromKey { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;

    public List<EmailRecipientResponse> To { get; set; } = new();
    public List<EmailRecipientResponse> Cc { get; set; } = new();
    public List<EmailRecipientResponse> Bcc { get; set; } = new();

    public List<EmailAttachmentResponse> Attachments { get; set; } = new();
}

public class EmailRecipientResponse
{
    public string Email { get; set; } = null!;
    public string Type { get; set; } = null!;
}

public class EmailAttachmentResponse
{
    public string Name { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public string Base64 { get; set; } = null!;
}