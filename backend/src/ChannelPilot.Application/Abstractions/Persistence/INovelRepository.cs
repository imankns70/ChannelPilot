using ChannelPilot.Domain.Novels;

namespace ChannelPilot.Application.Abstractions.Persistence;

public interface INovelRepository
{
    Task AddNovelAsync(Novel novel, CancellationToken cancellationToken = default);

    Task AddEpisodeAsync(NovelEpisode episode, CancellationToken cancellationToken = default);

    Task<Novel?> GetNovelByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<NovelEpisode?> GetNextUnpublishedEpisodeAsync(
        long novelId,
        CancellationToken cancellationToken = default);

    Task<bool> HasPublishedEpisodeTodayAsync(
        long novelId,
        DateOnly date,
        CancellationToken cancellationToken = default);
}
