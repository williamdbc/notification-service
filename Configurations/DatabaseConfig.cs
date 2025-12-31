using Microsoft.EntityFrameworkCore;
using NotificationService.Data;
using NotificationService.Settings;

namespace NotificationService.Configurations;

public static class DatabaseConfig
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var databaseSettings = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseSettings>();
        
        builder.Services.AddDbContext<NotificationDbContext>(options => options.UseNpgsql(databaseSettings.NotificationServiceConnection));
    }

    public static void InitializeDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
        dbContext.Database.Migrate();
        // dbContext.Database.EnsureCreated();
        // dbContext.Database.EnsureDeleted();
    }
}