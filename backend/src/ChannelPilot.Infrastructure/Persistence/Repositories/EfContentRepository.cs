using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Domain.Content;
using Microsoft.EntityFrameworkCore;

namespace ChannelPilot.Infrastructure.Persistence.Repositories;

public sealed class EfContentRepository(AppDbContext dbContext) : IContentRepository
{
    public async Task AddAsync(ContentItem item, CancellationToken cancellationToken = default)
    {
        await dbContext.ContentItems.AddAsync(item, cancellationToken);
    }

    public Task<ContentItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return dbContext.ContentItems.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
    }

    public Task<bool> ExistsByHashAsync(string contentHash, CancellationToken cancellationToken = default)
    {
        return dbContext.ContentItems.AnyAsync(
            item => item.ContentHash == contentHash,
            cancellationToken);
    }

    public async Task<IReadOnlyList<ContentItem>> GetPendingAsync(
        int take,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.ContentItems
            .Where(item => item.Status == ContentStatus.Collected)
            .OrderBy(item => item.CollectedAt)
            .Take(take)
            .ToListAsync(cancellationToken);
    }
}
