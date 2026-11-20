using ChannelPilot.Domain.Channels;

namespace ChannelPilot.Application.Abstractions.Persistence;

public interface IChannelRepository
{
    Task<IReadOnlyList<TelegramChannel>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TelegramChannel?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task AddAsync(TelegramChannel channel, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TelegramChannel>> GetActiveChannelsAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TelegramChannel>> GetActiveChannelsByTypeAsync(
        ChannelType type,
        CancellationToken cancellationToken = default);
}
