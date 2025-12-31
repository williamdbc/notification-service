using Microsoft.AspNetCore.Mvc;
using NotificationService.DTOs.Requests;
using NotificationService.Services.Interfaces;

namespace NotificationService.Controllers;

[ApiController]
[Route("notifications")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("email")]
    public async Task<IActionResult> CreateEmail([FromBody] CreateEmailNotificationRequest request)
    {
        var notification = await _notificationService.CreateEmail(request);
        return Ok(new { Id = notification.Id });
    }
    
    [HttpGet("{id:guid}/email")]
    public async Task<IActionResult> GetEmail(Guid id)
    {
        var response = await _notificationService.GetEmailById(id);
        return Ok(response);
    }

    [HttpGet("{id:guid}/sms")]
    public async Task<IActionResult> GetSms(Guid id)
    {
        var response = await _notificationService.GetSmsById(id);
        return Ok(response);
    }

    [HttpGet("{id:guid}/whatsapp")]
    public async Task<IActionResult> GetWhatsapp(Guid id)
    {
        var response = await _notificationService.GetWhatsappById(id);
        return Ok(response);
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        await _notificationService.Cancel(id);
        return NoContent();
    }
}
