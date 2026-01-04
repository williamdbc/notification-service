using NotificationService.Domain.Enums;

namespace NotificationService.Domain.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public NotificationChannel Channel { get; set; }
    public NotificationStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ScheduledAt { get; set; }
    public DateTimeOffset? SentAt { get; set; }
    
    public SmsPayload SmsPayload { get; set; }
    public EmailPayload EmailPayload { get; set; }
    public WhatsappPayload WhatsappPayload { get; set; }

    public bool CanBeSent() => Status == NotificationStatus.Pending;

    public void MarkAsSent()
    {
        Status = NotificationStatus.Sent;
        SentAt = DateTimeOffset.UtcNow;
    }
    
    public void Failed()
    {
        Status = NotificationStatus.Failed;
    }

    public void Cancel()
    {
        if (Status == NotificationStatus.Sent)
            throw new InvalidOperationException("Notification already sent");

        Status = NotificationStatus.Cancelled;
    }
    
    public void EnsureScheduledDateIsFuture()
    {
        if (ScheduledAt.HasValue && ScheduledAt.Value < DateTimeOffset.UtcNow)
            throw new InvalidOperationException("ScheduledAt must be a future date/time.");
    }
    
}