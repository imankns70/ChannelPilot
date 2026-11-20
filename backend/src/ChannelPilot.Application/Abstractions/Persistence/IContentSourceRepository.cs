using ChannelPilot.Domain.Content;

namespace ChannelPilot.Application.Abstractions.Persistence;

public interface IContentSourceRepository
{
    Task<IReadOnlyList<ContentSource>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<ContentSource?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task AddAsync(ContentSource source, CancellationToken cancellationToken = default);
}
