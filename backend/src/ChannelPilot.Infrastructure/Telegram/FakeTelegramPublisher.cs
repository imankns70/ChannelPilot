using ChannelPilot.Application.Abstractions.Publishing;
using ChannelPilot.Application.DTOs.Publishing;

namespace ChannelPilot.Infrastructure.Telegram;

public sealed class FakeTelegramPublisher : ITelegramPublisher
{
    private long _nextMessageId;

    public Task<PublishResultDto> PublishTextAsync(
        PublishRequestDto request,
        CancellationToken cancellationToken = default)
    {
        return CreateSuccessAsync(cancellationToken);
    }

    public Task<PublishResultDto> PublishPhotoAsync(
        PublishRequestDto request,
        CancellationToken cancellationToken = default)
    {
        return CreateSuccessAsync(cancellationToken);
    }

    private Task<PublishResultDto> CreateSuccessAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var messageId = Interlocked.Increment(ref _nextMessageId);
        return Task.FromResult(new PublishResultDto(true, messageId, null));
    }
}
