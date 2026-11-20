using ChannelPilot.Domain.Channels;
using ChannelPilot.Domain.Publishing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChannelPilot.Infrastructure.Persistence.Configurations;

public sealed class PublishedPostConfiguration : IEntityTypeConfiguration<PublishedPost>
{
    public void Configure(EntityTypeBuilder<PublishedPost> builder)
    {
        builder.ToTable("PublishedPosts");
        builder.HasKey(post => post.Id);

        builder.Property(post => post.Title).HasMaxLength(500).IsRequired();
        builder.Property(post => post.Caption).IsRequired();
        builder.Property(post => post.SourceUrl).HasMaxLength(2_000);

        builder.HasOne<TelegramChannel>()
            .WithMany()
            .HasForeignKey(post => post.TelegramChannelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<PublishQueueItem>()
            .WithMany()
            .HasForeignKey(post => post.PublishQueueItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(post => new { post.TelegramChannelId, post.PublishedAt });
    }
}
