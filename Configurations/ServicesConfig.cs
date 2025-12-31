using System.Text.Json.Serialization;
using NotificationService.Jobs;
using NotificationService.Repositories;
using NotificationService.Services;
using NotificationService.Services.Interfaces;

namespace NotificationService.Configurations;

public static class ServicesConfig
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();
        builder.Services.AddMemoryCache();
        
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<INotificationDispatcher, NotificationDispatcher>();
        builder.Services.AddScoped<NotificationJob>();
        builder.Services.AddScoped<INotificationService, Services.NotificationService>();
        builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<ISmsService, SmsService>();
        builder.Services.AddScoped<IWhatsAppService, WhatsAppService>();
        
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }
}