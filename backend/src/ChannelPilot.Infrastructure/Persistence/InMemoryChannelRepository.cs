using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Domain.Channels;

namespace ChannelPilot.Infrastructure.Persistence;

public sealed class InMemoryChannelRepository : IChannelRepository
{
    private readonly List<TelegramChannel> _channels = [];
    private readonly Lock _lock = new();
    private long _nextId;

    public Task<IReadOnlyList<TelegramChannel>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            IReadOnlyList<TelegramChannel> result = _channels.ToList();
            return Task.FromResult(result);
        }
    }

    public Task<TelegramChannel?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            return Task.FromResult(_channels.FirstOrDefault(channel => channel.Id == id));
        }
    }

    public Task AddAsync(
        TelegramChannel channel,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            if (channel.Id == 0)
            {
                channel.Id = ++_nextId;
            }

            _channels.Add(channel);
        }

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<TelegramChannel>> GetActiveChannelsAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            IReadOnlyList<TelegramChannel> result = _channels
                .Where(channel => channel.Status == ChannelStatus.Active)
                .ToList();
            return Task.FromResult(result);
        }
    }

    public Task<IReadOnlyList<TelegramChannel>> GetActiveChannelsByTypeAsync(
        ChannelType type,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            IReadOnlyList<TelegramChannel> result = _channels
                .Where(channel => channel.Status == ChannelStatus.Active && channel.Type == type)
                .ToList();
            return Task.FromResult(result);
        }
    }
}
