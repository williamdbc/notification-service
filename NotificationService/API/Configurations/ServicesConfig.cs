using System.Text.Json.Serialization;
using MediatR;
using NotificationService.Application.Abstractions.Dispatching;
using NotificationService.Application.Abstractions.Services;
using NotificationService.Domain.Dispatching;
using NotificationService.Domain.Repositories;
using NotificationService.Domain.Services;
using NotificationService.Infrastructure.Dispatching;
using NotificationService.Infrastructure.Jobs;
using NotificationService.Infrastructure.NotificationChannels.Email;
using NotificationService.Infrastructure.NotificationChannels.Sms;
using NotificationService.Infrastructure.NotificationChannels.Whatsapp;
using NotificationService.Infrastructure.Persistence.Repositories;

namespace NotificationService.API.Configurations;

public static class ServicesConfig
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();
        builder.Services.AddMemoryCache();
        builder.Services.AddMediatR(typeof(ServicesConfig));
        
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<INotificationSender, NotificationSender>();
        builder.Services.AddScoped<INotificationEnqueuer, HangfireNotificationEnqueuer>();
        builder.Services.AddScoped<NotificationJob>();
        builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<ISmsService, SmsService>();
        builder.Services.AddScoped<IWhatsappService, WhatsappService>();
        
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }
}