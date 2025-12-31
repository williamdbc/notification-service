namespace NotificationService.Configurations;

public static class MvcConfig
{
    public static void ConfigureMvc(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
    }
}