using ChannelPilot.Domain.Content;

namespace ChannelPilot.Application.Abstractions.Persistence;

public interface IContentRepository
{
    Task AddAsync(ContentItem item, CancellationToken cancellationToken = default);

    Task<ContentItem?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<bool> ExistsByHashAsync(string contentHash, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ContentItem>> GetPendingAsync(int take, CancellationToken cancellationToken = default);
}
