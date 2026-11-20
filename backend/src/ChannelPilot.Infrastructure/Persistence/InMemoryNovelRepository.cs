using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Domain.Novels;

namespace ChannelPilot.Infrastructure.Persistence;

public sealed class InMemoryNovelRepository : INovelRepository
{
    private readonly List<Novel> _novels = [];
    private readonly List<NovelEpisode> _episodes = [];
    private readonly Lock _lock = new();
    private long _nextNovelId;
    private long _nextEpisodeId;

    public Task AddNovelAsync(Novel novel, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            if (novel.Id == 0)
            {
                novel.Id = ++_nextNovelId;
            }

            _novels.Add(novel);
        }

        return Task.CompletedTask;
    }

    public Task AddEpisodeAsync(NovelEpisode episode, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            if (episode.Id == 0)
            {
                episode.Id = ++_nextEpisodeId;
            }

            _episodes.Add(episode);
        }

        return Task.CompletedTask;
    }

    public Task<Novel?> GetNovelByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            return Task.FromResult(_novels.FirstOrDefault(novel => novel.Id == id));
        }
    }

    public Task<NovelEpisode?> GetNextUnpublishedEpisodeAsync(
        long novelId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            var episode = _episodes
                .Where(item => item.NovelId == novelId && !item.IsPublished)
                .OrderBy(item => item.EpisodeNumber)
                .FirstOrDefault();
            return Task.FromResult(episode);
        }
    }

    public Task<bool> HasPublishedEpisodeTodayAsync(
        long novelId,
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (_lock)
        {
            var result = _episodes.Any(item =>
                item.NovelId == novelId &&
                item.PublishedAt.HasValue &&
                DateOnly.FromDateTime(item.PublishedAt.Value) == date);
            return Task.FromResult(result);
        }
    }
}
