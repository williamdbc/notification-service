using NotificationService.DTOs.Requests;
using NotificationService.DTOs.Responses;
using NotificationService.Entities;

namespace NotificationService.Services.Interfaces;

public interface INotificationService
{
    Task<Notification> CreateEmail(CreateEmailNotificationRequest request);
    
    Task<NotificationEmailResponse> GetEmailById(Guid id);
    Task<NotificationSmsResponse> GetSmsById(Guid id);
    Task<NotificationWhatsappResponse> GetWhatsappById(Guid id);
    Task Cancel(Guid id);
}