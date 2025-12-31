namespace NotificationService.DTOs.Requests;

public class CreateWhatsappNotificationRequest
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; } 
    public DateTime? ScheduledAt { get; set; }
}