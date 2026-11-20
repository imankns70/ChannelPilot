using ChannelPilot.Domain.Channels;
using ChannelPilot.Domain.Content;
using ChannelPilot.Domain.Novels;
using ChannelPilot.Domain.Publishing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChannelPilot.Infrastructure.Persistence.Configurations;

public sealed class PublishQueueItemConfiguration : IEntityTypeConfiguration<PublishQueueItem>
{
    public void Configure(EntityTypeBuilder<PublishQueueItem> builder)
    {
        builder.ToTable("PublishQueueItems");
        builder.HasKey(item => item.Id);

        builder.Property(item => item.Title).HasMaxLength(500).IsRequired();
        builder.Property(item => item.Caption).IsRequired();
        builder.Property(item => item.ImageUrl).HasMaxLength(2_000);
        builder.Property(item => item.Status).HasConversion<string>().HasMaxLength(30).IsRequired();
        builder.Property(item => item.FailureReason).HasConversion<string>().HasMaxLength(50);

        builder.HasOne<TelegramChannel>()
            .WithMany()
            .HasForeignKey(item => item.TelegramChannelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ContentItem>()
            .WithMany()
            .HasForeignKey(item => item.ContentItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<NovelEpisode>()
            .WithMany()
            .HasForeignKey(item => item.NovelEpisodeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(item => new { item.Status, item.ScheduledFor });
        builder.HasIndex(item => new { item.TelegramChannelId, item.PublishedAt });
    }
}
