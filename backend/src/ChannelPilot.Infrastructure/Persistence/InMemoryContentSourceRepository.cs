using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Domain.Content;

namespace ChannelPilot.Infrastructure.Persistence;

public sealed class InMemoryContentSourceRepository : IContentSourceRepository
{
    private readonly List<ContentSource> _sources = [];
    private readonly Lock _lock = new();
    private long _nextId;

    public Task<IReadOnlyList<ContentSource>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            return Task.FromResult<IReadOnlyList<ContentSource>>(_sources.ToList());
        }
    }

    public Task<ContentSource?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            return Task.FromResult(_sources.FirstOrDefault(source => source.Id == id));
        }
    }

    public Task AddAsync(ContentSource source, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            if (source.Id == 0)
            {
                source.Id = ++_nextId;
            }

            _sources.Add(source);
        }

        return Task.CompletedTask;
    }
}
