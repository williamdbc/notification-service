using System.ComponentModel.DataAnnotations.Schema;
using NotificationService.Domain.Enums;

namespace NotificationService.Domain.Entities;

public class EmailPayload
{
    public Guid Id { get; set; }
    public Guid NotificationId { get; set; }

    public string FromKey { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public ICollection<EmailRecipient> Recipients { get; set; } = new List<EmailRecipient>(); 
    public ICollection<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
    
    [NotMapped]
    public IEnumerable<EmailRecipient> To => Recipients.Where(r => r.Type == RecipientType.To);

    [NotMapped]
    public IEnumerable<EmailRecipient> Cc => Recipients.Where(r => r.Type == RecipientType.Cc);

    [NotMapped]
    public IEnumerable<EmailRecipient> Bcc => Recipients.Where(r => r.Type == RecipientType.Bcc);
}
