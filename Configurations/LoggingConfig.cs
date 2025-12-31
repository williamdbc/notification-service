using NotificationService.Clients;
using NotificationService.Settings;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Json;

namespace NotificationService.Configurations;

public static class LoggingConfig
{
    public static void ConfigureSerilog(this WebApplicationBuilder builder, string applicationName)
    {
        var logstashSettings = builder.Services.BuildServiceProvider().GetRequiredService<LogstashSettings>();

        var environment = builder.Environment.EnvironmentName;

        var envSuffix = environment.ToLower() switch
        {
            "production" => "prod",
            "development" => "hml",
            _ => environment.ToLower()
        };

        var bufferDir = Path.Combine(Path.GetTempPath(), "serilog-buffer", $"{applicationName}-{envSuffix}");
        Directory.CreateDirectory(bufferDir);
        var bufferPath = Path.Combine(bufferDir, "buffer");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Error)
            .Enrich.WithProperty("ApplicationName", $"{applicationName}-{envSuffix}")
            .Enrich.WithProperty("Environment", environment)
            .Enrich.WithExceptionDetails()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.DurableHttpUsingFileSizeRolledBuffers(
                requestUri: logstashSettings.Url,
                // httpClient: new LogstashHttpClient(logstashSettings),
                textFormatter: new JsonFormatter(renderMessage: true),
                bufferBaseFileName: bufferPath,
                bufferFileSizeLimitBytes: 2_097_152,
                retainedBufferFileCountLimit: 50,
                period: TimeSpan.FromSeconds(3))
            .CreateLogger();

        builder.Host.UseSerilog();
        Log.Information($"Buffer de logs configurado em: {bufferPath}");
    }

    public static void CloseAndFlush()
    {
        Log.CloseAndFlush();
    }
}