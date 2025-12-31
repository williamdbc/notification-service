using Microsoft.EntityFrameworkCore;
using NotificationService.Data;
using NotificationService.Entities;
using NotificationService.Enums;

namespace NotificationService.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly NotificationDbContext _context;

    public NotificationRepository(NotificationDbContext context)
    {
        _context = context;
    }

    public async Task Add(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync();
    }

    public async Task<Notification?> GetById(Guid id)
    {
        return await _context.Notifications
            .Include(n => n.EmailPayload)
            .Include(n => n.SmsPayload)
            .Include(n => n.WhatsappPayload)
            .FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task Update(Notification notification)
    {
        _context.Notifications.Update(notification);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Notification?> GetFullNotification(Guid id)
    {
        return await _context.Notifications
            .Include(n => n.EmailPayload)
                .ThenInclude(p => p.Recipients)
            .Include(n => n.EmailPayload)
                .ThenInclude(p => p.Attachments)
            .Include(n => n.SmsPayload)
            .Include(n => n.WhatsappPayload)
            .FirstOrDefaultAsync(n => n.Id == id);
    }
    
    public async Task<Notification?> GetEmailById(Guid id)
    {
        return await _context.Notifications
            .Include(n => n.EmailPayload)
            .ThenInclude(e => e.Recipients)
            .Include(n => n.EmailPayload)
            .ThenInclude(e => e.Attachments)
            .FirstOrDefaultAsync(n => n.Id == id && n.Channel == NotificationChannel.Email);
    }

    public async Task<Notification?> GetSmsById(Guid id)
    {
        return await _context.Notifications
            .Include(n => n.SmsPayload)
            .FirstOrDefaultAsync(n => n.Id == id && n.Channel == NotificationChannel.Sms);
    }

    public async Task<Notification?> GetWhatsappById(Guid id)
    {
        return await _context.Notifications
            .Include(n => n.WhatsappPayload)
            .FirstOrDefaultAsync(n => n.Id == id && n.Channel == NotificationChannel.WhatsApp);
    }
    
}