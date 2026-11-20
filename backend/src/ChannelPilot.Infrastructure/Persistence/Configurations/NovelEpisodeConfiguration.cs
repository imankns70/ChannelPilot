using ChannelPilot.Domain.Novels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChannelPilot.Infrastructure.Persistence.Configurations;

public sealed class NovelEpisodeConfiguration : IEntityTypeConfiguration<NovelEpisode>
{
    public void Configure(EntityTypeBuilder<NovelEpisode> builder)
    {
        builder.ToTable("NovelEpisodes");
        builder.HasKey(episode => episode.Id);

        builder.Property(episode => episode.Title).HasMaxLength(500).IsRequired();
        builder.Property(episode => episode.Content).IsRequired();
        builder.Property(episode => episode.HookText).HasMaxLength(1_000);
        builder.Property(episode => episode.EndingTeaser).HasMaxLength(1_000);

        builder.HasOne<Novel>()
            .WithMany()
            .HasForeignKey(episode => episode.NovelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(episode => new { episode.NovelId, episode.EpisodeNumber }).IsUnique();
    }
}
