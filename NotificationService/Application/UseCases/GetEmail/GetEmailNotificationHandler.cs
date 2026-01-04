using MediatR;
using NotificationService.Application.DTOs.Responses;
using NotificationService.Application.Mappers;
using NotificationService.Domain.Exceptions;
using NotificationService.Domain.Repositories;

namespace NotificationService.Application.UseCases.GetEmail;


public class GetEmailNotificationHandler : IRequestHandler<GetEmailNotificationQuery, NotificationEmailResponse>
{
    private readonly INotificationRepository _repository;

    public GetEmailNotificationHandler(
        INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<NotificationEmailResponse> Handle(
        GetEmailNotificationQuery request,
        CancellationToken ct)
    {
        var notification = await _repository.GetEmailById(request.NotificationId);

        if (notification?.EmailPayload == null)
            throw new NotFoundException("Email notification not found");

        return notification.ToResponse();
    }
}