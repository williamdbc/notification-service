namespace NotificationService.Entities;

public class SmsPayload
{
    public Guid Id { get; set; }
    public Guid NotificationId { get; set; }
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
}