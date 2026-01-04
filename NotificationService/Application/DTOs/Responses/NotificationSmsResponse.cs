using NotificationService.Domain.Entities;

namespace NotificationService.Application.DTOs.Responses;

public class NotificationSmsResponse
{
    public Guid Id { get; set; }
    public string Channel { get; set; } = "Sms";
    public string Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ScheduledAt { get; set; }
    public DateTimeOffset? SentAt { get; set; }
    
    public SmsPayload SmsPayload { get; set; }
}