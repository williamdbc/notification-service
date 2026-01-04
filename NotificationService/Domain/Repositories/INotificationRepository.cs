using NotificationService.Domain.Entities;

namespace NotificationService.Domain.Repositories;

public interface INotificationRepository
{
    Task Add(Notification notification);
    Task<Notification?> GetById(Guid id);
    Task Update(Notification notification);
    Task<Notification?> GetFullNotification(Guid id);
    Task<Notification?> GetEmailById(Guid id);
    Task<Notification?> GetSmsById(Guid id);
    Task<Notification?> GetWhatsappById(Guid id);
}