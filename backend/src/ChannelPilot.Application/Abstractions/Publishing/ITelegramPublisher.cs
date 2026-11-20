using ChannelPilot.Application.DTOs.Publishing;

namespace ChannelPilot.Application.Abstractions.Publishing;

public interface ITelegramPublisher
{
    Task<PublishResultDto> PublishTextAsync(
        PublishRequestDto request,
        CancellationToken cancellationToken = default);

    Task<PublishResultDto> PublishPhotoAsync(
        PublishRequestDto request,
        CancellationToken cancellationToken = default);
}
