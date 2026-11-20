namespace ChannelPilot.Domain.Publishing;

public enum PublishFailureReason
{
    None,
    TelegramError,
    RateLimited,
    MissingContent,
    NotApproved,
    Unknown
}
