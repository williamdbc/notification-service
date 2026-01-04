using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.UseCases.CancelNotification;
using NotificationService.Application.UseCases.CreateEmail;
using NotificationService.Application.UseCases.GetEmail;

namespace NotificationService.API.Controllers;

[ApiController]
[Route("notifications")]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("email")]
    public async Task<IActionResult> CreateEmail(
        [FromBody] CreateEmailNotificationCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { Id = id });
    }

    [HttpGet("{id:guid}/email")]
    public async Task<IActionResult> GetEmail(Guid id)
    {
        var response = await _mediator.Send(new GetEmailNotificationQuery(id));
        return Ok(response);
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        await _mediator.Send(new CancelNotificationCommand(id));
        return NoContent();
    }
}
