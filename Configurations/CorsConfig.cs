using NotificationService.Settings;

namespace NotificationService.Configurations;

public static class CorsConfig
{
    private static readonly string _policyName = "Origins";

    public static void ConfigureCors(this WebApplicationBuilder builder)
    {
        var corsSettings = builder.Services.BuildServiceProvider().GetRequiredService<CorsSettings>();
        var allowedOrigins = corsSettings.AllowedOrigins ?? Array.Empty<string>();
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: _policyName,
                policy =>
                {
                    if (builder.Environment.IsDevelopment())
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }
                    else
                    {
                        policy.WithOrigins(allowedOrigins)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    }
                });
        });
    }

    public static void UseCorsConfiguration(this WebApplication app)
    {
        app.UseCors(_policyName);
    }
}