using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Domain.Channels;
using Microsoft.EntityFrameworkCore;

namespace ChannelPilot.Infrastructure.Persistence.Repositories;

public sealed class EfChannelRepository(AppDbContext dbContext) : IChannelRepository
{
    public async Task<IReadOnlyList<TelegramChannel>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbContext.TelegramChannels
            .OrderBy(channel => channel.Type)
            .ThenBy(channel => channel.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<TelegramChannel?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return dbContext.TelegramChannels.FirstOrDefaultAsync(
            channel => channel.Id == id,
            cancellationToken);
    }

    public async Task AddAsync(
        TelegramChannel channel,
        CancellationToken cancellationToken = default)
    {
        await dbContext.TelegramChannels.AddAsync(channel, cancellationToken);
    }

    public async Task<IReadOnlyList<TelegramChannel>> GetActiveChannelsAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbContext.TelegramChannels
            .Where(channel => channel.Status == ChannelStatus.Active)
            .OrderBy(channel => channel.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TelegramChannel>> GetActiveChannelsByTypeAsync(
        ChannelType type,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.TelegramChannels
            .Where(channel => channel.Status == ChannelStatus.Active && channel.Type == type)
            .OrderBy(channel => channel.Name)
            .ToListAsync(cancellationToken);
    }
}
