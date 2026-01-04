using Hangfire;
using Hangfire.PostgreSql;
using NotificationService.API.Settings;

namespace NotificationService.API.Configurations;

public static class HangfireConfig
{
    public static void ConfigureHangfire(this WebApplicationBuilder builder)
    {
        var settings = builder.Services
            .BuildServiceProvider()
            .GetRequiredService<HangfireSettings>();

        builder.Services.AddHangfire(config =>
        {
            config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(
                    settings.ConnectionString,
                    new PostgreSqlStorageOptions
                    {
                        SchemaName = settings.Schema,
                        PrepareSchemaIfNecessary = true
                    });
        });

        builder.Services.AddHangfireServer(options =>
        {
            options.WorkerCount = settings.WorkerCount;
            options.Queues = settings.Queues;
        });
    }

    public static void UseHangfire(this WebApplication app)
    {
        var settings = app.Services.GetRequiredService<HangfireSettings>();

        if (!settings.EnableDashboard)
            return;

        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "Hangfire Dashboard",
            Authorization = new[] { new HangfireAuthorizationFilter() }
        });
    }
}