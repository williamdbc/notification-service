using Hangfire;
using Hangfire.Server;
using NotificationService.Domain.Dispatching;
using NotificationService.Domain.Enums;
using NotificationService.Domain.Repositories;

namespace NotificationService.Infrastructure.Jobs;

public class NotificationJob
{
    private readonly INotificationSender _notificationSender;
    private readonly INotificationRepository _notificationRepository;

    public NotificationJob(
        INotificationSender notificationSender,
        INotificationRepository notificationRepository)
    {
        _notificationSender = notificationSender;
        _notificationRepository = notificationRepository;
    }

    [AutomaticRetry(Attempts = 3, DelaysInSeconds = new[] { 30, 30, 30 })]
    public async Task Send(Guid notificationId, PerformContext? context = null)
    {
        try
        {
            await _notificationSender.Dispatch(notificationId);
        }
        catch (Exception)
        {
            if (context != null && IsLastRetryAttempt(context))
            {
                var notification = await _notificationRepository.GetById(notificationId);
                if (notification != null && notification.Status != NotificationStatus.Sent)
                {
                    notification.Failed();
                    await _notificationRepository.Update(notification);
                }
            }

            throw;
        }
    }

    private static bool IsLastRetryAttempt(PerformContext context)
    {
        var automaticRetryAttr = context.BackgroundJob.Job.Method
            .GetCustomAttributes(typeof(AutomaticRetryAttribute), false)
            .FirstOrDefault() as AutomaticRetryAttribute;

        int maxAttempts = automaticRetryAttr?.Attempts ?? 10;
        
        if (maxAttempts <= 0) maxAttempts = 10;
      
        var retryCount = context.GetJobParameter<int>("RetryCount");
        
        return retryCount == maxAttempts - 1;
    }
}