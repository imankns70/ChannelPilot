using ChannelPilot.Domain.Channels;
using Microsoft.EntityFrameworkCore;

namespace ChannelPilot.Infrastructure.Persistence.Seeding;

public sealed class DatabaseSeeder(AppDbContext dbContext)
{
    private const long FootballPlaceholderChatId = -9_000_000_000_001;
    private const long TrendPlaceholderChatId = -9_000_000_000_002;
    private const long NovelPlaceholderChatId = -9_000_000_000_003;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var existingTypes = await dbContext.TelegramChannels
            .Select(channel => channel.Type)
            .ToListAsync(cancellationToken);

        var types = existingTypes.ToHashSet();
        var createdAt = DateTime.UtcNow;

        if (!types.Contains(ChannelType.Football))
        {
            dbContext.TelegramChannels.Add(CreateFootballChannel(createdAt));
        }

        if (!types.Contains(ChannelType.Trend))
        {
            dbContext.TelegramChannels.Add(CreateTrendChannel(createdAt));
        }

        if (!types.Contains(ChannelType.Novel))
        {
            dbContext.TelegramChannels.Add(CreateNovelChannel(createdAt));
        }

        if (dbContext.ChangeTracker.HasChanges())
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    private static TelegramChannel CreateFootballChannel(DateTime createdAt) => new()
    {
        Name = "Football Channel",
        TelegramChatId = FootballPlaceholderChatId,
        Username = "football_channel_placeholder",
        Type = ChannelType.Football,
        Status = ChannelStatus.Active,
        RequiresApproval = false,
        MinScoreToPublish = 70,
        MaxPostsPerDay = 40,
        MinMinutesBetweenPosts = 10,
        FixedPublishTime = null,
        CreatedAt = createdAt
    };

    private static TelegramChannel CreateTrendChannel(DateTime createdAt) => new()
    {
        Name = "Trend News Channel",
        TelegramChatId = TrendPlaceholderChatId,
        Username = "trend_channel_placeholder",
        Type = ChannelType.Trend,
        Status = ChannelStatus.Active,
        RequiresApproval = true,
        MinScoreToPublish = 85,
        MaxPostsPerDay = 15,
        MinMinutesBetweenPosts = 30,
        FixedPublishTime = null,
        CreatedAt = createdAt
    };

    private static TelegramChannel CreateNovelChannel(DateTime createdAt) => new()
    {
        Name = "Novel Channel",
        TelegramChatId = NovelPlaceholderChatId,
        Username = "novel_channel_placeholder",
        Type = ChannelType.Novel,
        Status = ChannelStatus.Active,
        RequiresApproval = false,
        MinScoreToPublish = 0,
        MaxPostsPerDay = 1,
        MinMinutesBetweenPosts = 1_440,
        FixedPublishTime = new TimeOnly(22, 30),
        CreatedAt = createdAt
    };
}
