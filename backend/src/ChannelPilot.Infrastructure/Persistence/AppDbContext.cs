using ChannelPilot.Domain.Channels;
using ChannelPilot.Domain.Content;
using ChannelPilot.Domain.Football;
using ChannelPilot.Domain.Novels;
using ChannelPilot.Domain.Publishing;
using ChannelPilot.Domain.Trends;
using Microsoft.EntityFrameworkCore;

namespace ChannelPilot.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TelegramChannel> TelegramChannels => Set<TelegramChannel>();

    public DbSet<ContentSource> ContentSources => Set<ContentSource>();

    public DbSet<ContentItem> ContentItems => Set<ContentItem>();

    public DbSet<FootballNewsItem> FootballNewsItems => Set<FootballNewsItem>();

    public DbSet<TrendNewsItem> TrendNewsItems => Set<TrendNewsItem>();

    public DbSet<TrendSignal> TrendSignals => Set<TrendSignal>();

    public DbSet<Novel> Novels => Set<Novel>();

    public DbSet<NovelEpisode> NovelEpisodes => Set<NovelEpisode>();

    public DbSet<NovelPublishingPlan> NovelPublishingPlans => Set<NovelPublishingPlan>();

    public DbSet<PublishQueueItem> PublishQueueItems => Set<PublishQueueItem>();

    public DbSet<PublishedPost> PublishedPosts => Set<PublishedPost>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
