using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Domain.Publishing;
using Microsoft.EntityFrameworkCore;

namespace ChannelPilot.Infrastructure.Persistence.Repositories;

public sealed class EfPublishQueueRepository(AppDbContext dbContext) : IPublishQueueRepository
{
    public async Task AddAsync(PublishQueueItem item, CancellationToken cancellationToken = default)
    {
        await dbContext.PublishQueueItems.AddAsync(item, cancellationToken);
    }

    public async Task<IReadOnlyList<PublishQueueItem>> GetDueItemsAsync(
        DateTime dueAt,
        int take,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.PublishQueueItems
            .Where(item =>
                (item.Status == PublishStatus.Pending ||
                 item.Status == PublishStatus.Approved ||
                 item.Status == PublishStatus.Scheduled) &&
                (!item.ScheduledFor.HasValue || item.ScheduledFor <= dueAt))
            .OrderBy(item => item.ScheduledFor ?? item.CreatedAt)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public Task<PublishQueueItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return dbContext.PublishQueueItems.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
    }

    public Task<bool> HasPublishedTodayAsync(
        long telegramChannelId,
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        var start = date.ToDateTime(TimeOnly.MinValue);
        var end = start.AddDays(1);

        return dbContext.PublishQueueItems.AnyAsync(
            item => item.TelegramChannelId == telegramChannelId &&
                    item.PublishedAt >= start &&
                    item.PublishedAt < end,
            cancellationToken);
    }
}
