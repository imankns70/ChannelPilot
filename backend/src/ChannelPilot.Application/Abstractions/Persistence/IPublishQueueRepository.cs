using ChannelPilot.Domain.Publishing;

namespace ChannelPilot.Application.Abstractions.Persistence;

public interface IPublishQueueRepository
{
    Task AddAsync(PublishQueueItem item, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PublishQueueItem>> GetDueItemsAsync(
        DateTime dueAt,
        int take,
        CancellationToken cancellationToken = default);

    Task<PublishQueueItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<bool> HasPublishedTodayAsync(
        long telegramChannelId,
        DateOnly date,
        CancellationToken cancellationToken = default);
}
