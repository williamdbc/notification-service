using NotificationService.API.Extensions;
using NotificationService.API.Settings;

namespace NotificationService.API.Configurations;

public static class SettingsConfig
{
    public static void ConfigureSettings(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatedSingletonSettings<LogstashSettings>(builder.Configuration, "LogstashSettings");
        builder.Services.AddValidatedSingletonSettings<CorsSettings>(builder.Configuration, "CorsSettings");
        builder.Services.AddValidatedSingletonSettings<DatabaseSettings>(builder.Configuration, "DatabaseSettings");
        builder.Services.AddValidatedSingletonSettings<EmailsSettings>(builder.Configuration, "EmailsSettings");
        builder.Services.AddValidatedSingletonSettings<HangfireSettings>(builder.Configuration, "HangfireSettings");
    }
}