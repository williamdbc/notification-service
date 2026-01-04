using MediatR;
using NotificationService.Application.DTOs.Responses;

namespace NotificationService.Application.UseCases.GetEmail;

public class GetEmailNotificationQuery : IRequest<NotificationEmailResponse>
{
    public Guid NotificationId { get; }

    public GetEmailNotificationQuery(Guid notificationId)
    {
        NotificationId = notificationId;
    }
}