using ChannelPilot.Application.Abstractions.Clock;
using ChannelPilot.Application.Abstractions.Persistence;
using ChannelPilot.Domain.Content;

namespace ChannelPilot.Application.ContentSources;

public sealed class ContentSourceManagementService(
    IContentSourceRepository contentSourceRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
{
    public Task<IReadOnlyList<ContentSource>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return contentSourceRepository.GetAllAsync(cancellationToken);
    }

    public Task<ContentSource?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return contentSourceRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<ContentSource> CreateAsync(
        ContentSource source,
        CancellationToken cancellationToken = default)
    {
        source.CreatedAt = dateTimeProvider.UtcNow;
        await contentSourceRepository.AddAsync(source, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return source;
    }

    public async Task<ContentSource?> UpdateAsync(
        long id,
        ContentSource values,
        CancellationToken cancellationToken = default)
    {
        var source = await contentSourceRepository.GetByIdAsync(id, cancellationToken);
        if (source is null)
        {
            return null;
        }

        source.Name = values.Name;
        source.BaseUrl = values.BaseUrl;
        source.SourceKind = values.SourceKind;
        source.IsActive = values.IsActive;
        source.ReliabilityScore = values.ReliabilityScore;
        source.LastFetchedAt = values.LastFetchedAt;
        source.FailureCount = values.FailureCount;
        source.UpdatedAt = dateTimeProvider.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return source;
    }

    public Task<ContentSource?> ActivateAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return SetActiveAsync(id, true, cancellationToken);
    }

    public Task<ContentSource?> DisableAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        return SetActiveAsync(id, false, cancellationToken);
    }

    private async Task<ContentSource?> SetActiveAsync(
        long id,
        bool isActive,
        CancellationToken cancellationToken)
    {
        var source = await contentSourceRepository.GetByIdAsync(id, cancellationToken);
        if (source is null)
        {
            return null;
        }

        source.IsActive = isActive;
        source.UpdatedAt = dateTimeProvider.UtcNow;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return source;
    }
}
