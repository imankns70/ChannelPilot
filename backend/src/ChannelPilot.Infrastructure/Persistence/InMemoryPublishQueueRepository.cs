using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Domain.Publishing;

namespace ChannelPilot.Infrastructure.Persistence;

public sealed class InMemoryPublishQueueRepository : IPublishQueueRepository
{
    private readonly List<PublishQueueItem> _items = [];
    private readonly Lock _lock = new();
    private long _nextId;

    public Task AddAsync(PublishQueueItem item, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            if (item.Id == 0)
            {
                item.Id = ++_nextId;
            }

            _items.Add(item);
        }

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<PublishQueueItem>> GetDueItemsAsync(
        DateTime dueAt,
        int take,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            IReadOnlyList<PublishQueueItem> result = _items
                .Where(item =>
                    (item.Status is PublishStatus.Pending or PublishStatus.Approved or PublishStatus.Scheduled) &&
                    (!item.ScheduledFor.HasValue || item.ScheduledFor <= dueAt))
                .OrderBy(item => item.ScheduledFor ?? item.CreatedAt)
                .Take(take)
                .ToList();
            return Task.FromResult(result);
        }
    }

    public Task<PublishQueueItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            return Task.FromResult(_items.FirstOrDefault(item => item.Id == id));
        }
    }

    public Task<bool> HasPublishedTodayAsync(
        long telegramChannelId,
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            var result = _items.Any(item =>
                item.TelegramChannelId == telegramChannelId &&
                item.PublishedAt.HasValue &&
                DateOnly.FromDateTime(item.PublishedAt.Value) == date);
            return Task.FromResult(result);
        }
    }
}
