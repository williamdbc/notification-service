namespace NotificationService.Settings;

public class EmailSettings
{
    public string FromKey { get; set; } = string.Empty;
    public string SmtpServer { get; set; } = string.Empty;
    public string Port { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromAddress { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
}

public class EmailsSettings : List<EmailSettings> { }