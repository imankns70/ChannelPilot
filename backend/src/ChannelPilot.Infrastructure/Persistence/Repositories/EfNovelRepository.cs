using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Domain.Novels;
using Microsoft.EntityFrameworkCore;

namespace ChannelPilot.Infrastructure.Persistence.Repositories;

public sealed class EfNovelRepository(AppDbContext dbContext) : INovelRepository
{
    public async Task AddNovelAsync(Novel novel, CancellationToken cancellationToken = default)
    {
        await dbContext.Novels.AddAsync(novel, cancellationToken);
    }

    public async Task AddEpisodeAsync(NovelEpisode episode, CancellationToken cancellationToken = default)
    {
        await dbContext.NovelEpisodes.AddAsync(episode, cancellationToken);
    }

    public Task<Novel?> GetNovelByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return dbContext.Novels.FirstOrDefaultAsync(novel => novel.Id == id, cancellationToken);
    }

    public Task<NovelEpisode?> GetNextUnpublishedEpisodeAsync(
        long novelId,
        CancellationToken cancellationToken = default)
    {
        return dbContext.NovelEpisodes
            .Where(episode => episode.NovelId == novelId && !episode.IsPublished)
            .OrderBy(episode => episode.EpisodeNumber)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> HasPublishedEpisodeTodayAsync(
        long novelId,
        DateOnly date,
        CancellationToken cancellationToken = default)
    {
        var start = date.ToDateTime(TimeOnly.MinValue);
        var end = start.AddDays(1);

        return dbContext.NovelEpisodes.AnyAsync(
            episode => episode.NovelId == novelId &&
                       episode.PublishedAt >= start &&
                       episode.PublishedAt < end,
            cancellationToken);
    }
}
