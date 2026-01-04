using NotificationService.Domain.Entities;

namespace NotificationService.Application.DTOs.Responses;

public class NotificationWhatsappResponse
{
    public Guid Id { get; set; }
    public string Channel { get; set; } = "WhatsApp";
    public string Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ScheduledAt { get; set; }
    public DateTimeOffset? SentAt { get; set; }
    
    public WhatsappPayload WhatsappPayload { get; set; }
}