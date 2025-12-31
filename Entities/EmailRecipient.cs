using NotificationService.Enums;

namespace NotificationService.Entities;

public class EmailRecipient
{
    public Guid Id { get; set; }
    
    public Guid EmailPayloadId { get; set; }
    public EmailPayload EmailPayload { get; set; }
    
    public string Email { get; set; }
    public RecipientType Type { get; set; }
}
