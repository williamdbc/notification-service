namespace NotificationService.Domain.Entities;

public class EmailAttachment
{
    public Guid Id { get; set; }
    
    public Guid EmailPayloadId { get; set; }
    public EmailPayload EmailPayload { get; set; }
    
    public string Name { get; set; }
    public string ContentType { get; set; }
    public string Base64 { get; set; }
    
}