using Microsoft.EntityFrameworkCore;
using NotificationService.Entities;

namespace NotificationService.Data;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }
    
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<EmailPayload> EmailPayloads { get; set; } = null!;
    public DbSet<SmsPayload> SmsPayloads { get; set; } = null!;
    public DbSet<WhatsappPayload> WhatsappPayloads { get; set; } = null!;
    public DbSet<EmailRecipient> EmailRecipients { get; set; } = null!;
    public DbSet<EmailAttachment> EmailAttachments { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EmailRecipient>()
            .HasOne(er => er.EmailPayload)
            .WithMany(ep => ep.Recipients)
            .HasForeignKey(er => er.EmailPayloadId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EmailAttachment>()
            .HasOne(ea => ea.EmailPayload)
            .WithMany(ep => ep.Attachments)
            .HasForeignKey(ea => ea.EmailPayloadId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}