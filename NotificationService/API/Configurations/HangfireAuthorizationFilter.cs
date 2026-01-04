using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace NotificationService.API.Configurations;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        var env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

        return env.IsDevelopment();
    }
}