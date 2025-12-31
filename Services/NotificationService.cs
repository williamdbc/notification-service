using Hangfire;
using NotificationService.DTOs.Requests;
using NotificationService.DTOs.Responses;
using NotificationService.Entities;
using NotificationService.Enums;
using NotificationService.Exceptions;
using NotificationService.Jobs;
using NotificationService.Mappers;
using NotificationService.Repositories;
using NotificationService.Services.Interfaces;

namespace NotificationService.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<Notification> CreateEmail(CreateEmailNotificationRequest request)
    {
        var emailPayload = request.ToEntity();

        if (request.Attachments != null)
            foreach (var attachment in request.Attachments)
                emailPayload.Attachments.Add(attachment.ToEntity());

        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            Channel = NotificationChannel.Email,
            ScheduledAt = request.ScheduledAt,
            EmailPayload = emailPayload
        };
        
        notification.EnsureScheduledDateIsFuture(); 

        notification.EmailPayload = emailPayload;
        
        await _notificationRepository.Add(notification);
        EnqueueNotification(notification);
        return notification;
    }
    
    public async Task<Notification?> GetById(Guid id)
    {
        var notification = await _notificationRepository.GetById(id);
        if (notification == null)
            throw new NotFoundException("Notification not found");
        
        return notification;
    }

    public async Task Cancel(Guid id)
    {
        var notification = await _notificationRepository.GetById(id);

        if (notification.Status == NotificationStatus.Sent)
            throw new BusinessException("Notification already sent and cannot be canceled");

        notification.Cancel();

        await _notificationRepository.Update(notification);
    }
    
    private void EnqueueNotification(Notification notification)
    {
        var queue = NotificationQueueResolver.Resolve(notification.Channel);

        if (notification.ScheduledAt.HasValue)
        {
            BackgroundJob.Schedule<NotificationJob>(
                queue,
                job => job.Send(notification.Id, null),
                notification.ScheduledAt.Value);
        }
        else
        {
            BackgroundJob.Enqueue<NotificationJob>(
                queue,
                job => job.Send(notification.Id, null));
        }
    }
    
    public async Task<NotificationEmailResponse> GetEmailById(Guid id)
    {
        var notification = await _notificationRepository.GetEmailById(id);
        if (notification == null || notification.EmailPayload == null)
            throw new NotFoundException("Email notification not found");

        return notification.ToEmailResponse();
    }

    public async Task<NotificationSmsResponse> GetSmsById(Guid id)
    {
        var notification = await _notificationRepository.GetSmsById(id);
        if (notification == null || notification.SmsPayload == null)
            throw new NotFoundException("SMS notification not found");

        return notification.ToSmsResponse();
    }

    public async Task<NotificationWhatsappResponse> GetWhatsappById(Guid id)
    {
        var notification = await _notificationRepository.GetWhatsappById(id);
        if (notification == null || notification.WhatsappPayload == null)
            throw new NotFoundException("Whatsapp notification not found");

        return notification.ToWhatsappResponse();
    }
    
}