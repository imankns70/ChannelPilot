using ChannelPilot.Application.Abstractions.Clock;
using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Domain.Channels;

namespace ChannelPilot.Application.Channels;

public sealed class ChannelManagementService(
    IChannelRepository channelRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
{
    public Task<IReadOnlyList<TelegramChannel>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return channelRepository.GetAllAsync(cancellationToken);
    }

    public Task<TelegramChannel?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return channelRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<TelegramChannel> CreateAsync(
        TelegramChannel channel,
        CancellationToken cancellationToken = default)
    {
        channel.Status = ChannelStatus.Active;
        channel.CreatedAt = dateTimeProvider.UtcNow;

        await channelRepository.AddAsync(channel, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return channel;
    }

    public async Task<TelegramChannel?> UpdateAsync(
        long id,
        TelegramChannel values,
        CancellationToken cancellationToken = default)
    {
        var channel = await channelRepository.GetByIdAsync(id, cancellationToken);
        if (channel is null)
        {
            return null;
        }

        channel.Name = values.Name;
        channel.TelegramChatId = values.TelegramChatId;
        channel.Username = values.Username;
        channel.Type = values.Type;
        channel.Status = values.Status;
        channel.RequiresApproval = values.RequiresApproval;
        channel.MinScoreToPublish = values.MinScoreToPublish;
        channel.MaxPostsPerDay = values.MaxPostsPerDay;
        channel.MinMinutesBetweenPosts = values.MinMinutesBetweenPosts;
        channel.FixedPublishTime = values.FixedPublishTime;
        channel.UpdatedAt = dateTimeProvider.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return channel;
    }

    public async Task<TelegramChannel?> SetStatusAsync(
        long id,
        ChannelStatus status,
        CancellationToken cancellationToken = default)
    {
        var channel = await channelRepository.GetByIdAsync(id, cancellationToken);
        if (channel is null)
        {
            return null;
        }

        channel.Status = status;
        channel.UpdatedAt = dateTimeProvider.UtcNow;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return channel;
    }
}
