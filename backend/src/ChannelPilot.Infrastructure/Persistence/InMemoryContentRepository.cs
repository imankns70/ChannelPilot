using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Domain.Content;

namespace ChannelPilot.Infrastructure.Persistence;

public sealed class InMemoryContentRepository : IContentRepository
{
    private readonly List<ContentItem> _items = [];
    private readonly Lock _lock = new();
    private long _nextId;

    public Task AddAsync(ContentItem item, CancellationToken cancellationToken = default)
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

    public Task<ContentItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            return Task.FromResult(_items.FirstOrDefault(item => item.Id == id));
        }
    }

    public Task<bool> ExistsByHashAsync(string contentHash, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            return Task.FromResult(_items.Any(item => item.ContentHash == contentHash));
        }
    }

    public Task<IReadOnlyList<ContentItem>> GetPendingAsync(
        int take,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            IReadOnlyList<ContentItem> result = _items
                .Where(item => item.Status == ContentStatus.Collected)
                .Take(take)
                .ToList();
            return Task.FromResult(result);
        }
    }
}
