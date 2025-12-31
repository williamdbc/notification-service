namespace NotificationService.Settings;

public class HangfireSettings
{
    public string ConnectionString { get; set; } = null!;
    public string Schema { get; set; } = "hangfire";
    public int WorkerCount { get; set; } = Environment.ProcessorCount * 2;
    public string[] Queues { get; set; } = ["email", "sms", "whatsapp", "default"];
    public bool EnableDashboard { get; set; } = false;
}