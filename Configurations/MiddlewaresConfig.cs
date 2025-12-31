using NotificationService.Middlewares;

namespace NotificationService.Configurations;

public static class MiddlewaresConfig
{
    public static void ConfigureMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();
        
        app.UseRouting();
        
        app.UseCorsConfiguration();
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapHealthChecks("/health");
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.MapControllers();
    }
}