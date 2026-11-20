namespace ChannelPilot.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }

    DateTime Now { get; }

    DateOnly Today { get; }

    TimeOnly CurrentTime { get; }
}
