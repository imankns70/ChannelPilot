using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Domain.Content;
using Microsoft.EntityFrameworkCore;

namespace ChannelPilot.Infrastructure.Persistence.Repositories;

public sealed class EfContentSourceRepository(AppDbContext dbContext) : IContentSourceRepository
{
    public async Task<IReadOnlyList<ContentSource>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbContext.ContentSources
            .OrderBy(source => source.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<ContentSource?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return dbContext.ContentSources.FirstOrDefaultAsync(
            source => source.Id == id,
            cancellationToken);
    }

    public async Task AddAsync(
        ContentSource source,
        CancellationToken cancellationToken = default)
    {
        await dbContext.ContentSources.AddAsync(source, cancellationToken);
    }
}
